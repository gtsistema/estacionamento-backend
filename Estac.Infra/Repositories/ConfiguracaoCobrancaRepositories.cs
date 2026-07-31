using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class ConfiguracaoCobrancaRepositories : BaseRepositories<ConfiguracaoCobranca>, IConfiguracaoCobrancaRepositories
    {
        private readonly DbSet<ConfiguracaoCobranca> _dataset;

        public ConfiguracaoCobrancaRepositories(GtsContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<ConfiguracaoCobranca>();
        }

        public async Task<ConfiguracaoCobranca> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                .AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento)
                .Include(x => x.ConfiguracaoAgendamento)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<ConfiguracaoCobrancaSearchOutput>> Paginar(ConfiguracaoCobrancaFilterInput input)
        {
            var query = _dataset
                .AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento)
                .AsQueryable();

            if (input.TransportadoraId.HasValue)
                query = query.Where(x => x.TransportadoraId == input.TransportadoraId.Value);

            if (input.EstacionamentoId.HasValue)
                query = query.Where(x => x.EstacionamentoId == input.EstacionamentoId.Value);

            if (input.Status.HasValue)
                query = query.Where(x => x.Status == input.Status.Value);

            if (!string.IsNullOrWhiteSpace(input.Descricao))
            {
                var termo = input.Descricao.Trim().ToLower();
                query = query.Where(x =>
                    (x.EmailFinanceiro != null && x.EmailFinanceiro.ToLower().Contains(termo))
                    || (x.Transportadora != null && x.Transportadora.Descricao != null && x.Transportadora.Descricao.ToLower().Contains(termo))
                    || (x.Estacionamento != null && x.Estacionamento.Descricao != null && x.Estacionamento.Descricao.ToLower().Contains(termo)));
            }

            if (input.DataInicial.HasValue)
                query = query.Where(x => x.DataCriacao.Date >= input.DataInicial.Value.Date);

            if (input.DataFinal.HasValue)
                query = query.Where(x => x.DataCriacao.Date <= input.DataFinal.Value.Date);

            return await query
                .OrderByDescending(x => x.DataCriacao)
                .Select(x => new ConfiguracaoCobrancaSearchOutput
                {
                    Id = x.Id,
                    TransportadoraId = x.TransportadoraId,
                    TransportadoraNome = x.Transportadora.Descricao,
                    EstacionamentoId = x.EstacionamentoId,
                    EstacionamentoNome = x.Estacionamento.Descricao,
                    Status = x.Status,
                    ModalidadeCobranca = x.ModalidadeCobranca,
                    DiaFechamento = x.DiaFechamento,
                    RegraFechamento = x.RegraFechamento,
                    PrazoVencimentoDias = x.PrazoVencimentoDias,
                    ValorEstacionamento = x.ValorEstacionamento,
                    EmailFinanceiro = x.EmailFinanceiro,
                    EnvioAutomaticoEmail = x.EnvioAutomaticoEmail,
                    GerarFaturaAutomaticamente = x.GerarFaturaAutomaticamente,
                    DataCriacao = x.DataCriacao
                })
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }

        public Task<bool> ExistePorTransportadoraEstacionamentoAsync(int transportadoraId, int estacionamentoId, int? ignorarId = null)
        {
            return _dataset
                .AsNoTracking()
                .AnyAsync(x =>
                    x.TransportadoraId == transportadoraId
                    && x.EstacionamentoId == estacionamentoId
                    && (!ignorarId.HasValue || x.Id != ignorarId.Value));
        }

        public override async Task<ConfiguracaoCobranca> Alterar(ConfiguracaoCobranca item)
        {
            try
            {
                var incomingAgendamento = item.ConfiguracaoAgendamento;
                item.ConfiguracaoAgendamento = null;

                var result = await _dataset
                    .Include(x => x.ConfiguracaoAgendamento)
                    .SingleOrDefaultAsync(p => p.Id == item.Id);

                if (result == null)
                    return null;

                item.DataCriacao = result.DataCriacao;
                item.DataAtualizacao = DateTime.Now;
                _context.Entry(result).CurrentValues.SetValues(item);

                SincronizarAgendamento(result, incomingAgendamento);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task Remove(int id)
        {
            try
            {
                var configuracao = await _dataset
                    .Include(x => x.ConfiguracaoAgendamento)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (configuracao is null)
                    return;

                if (configuracao.ConfiguracaoAgendamento is not null)
                    _context.Remove(configuracao.ConfiguracaoAgendamento);

                _context.Remove(configuracao);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        /// <summary>
        /// Preserva o registro existente para não perder UltimaExecucao/ProximaExecucao:
        /// sem agendamento novo, apenas inativa o atual.
        /// </summary>
        private void SincronizarAgendamento(
            ConfiguracaoCobranca configuracao,
            ConfiguracaoAgendamento incoming)
        {
            var agora = DateTime.Now;
            var existente = configuracao.ConfiguracaoAgendamento;

            if (incoming is null)
            {
                if (existente is not null && existente.Ativo)
                {
                    existente.Ativo = false;
                    existente.DataAtualizacao = agora;
                }

                return;
            }

            if (existente is null)
            {
                // Guid já preenchido + FK preenchida faz o EF marcar como Modified (UPDATE 0 linhas).
                // Add explícito garante INSERT.
                if (incoming.Id == Guid.Empty)
                    incoming.Id = Guid.NewGuid();

                incoming.ConfiguracaoCobrancaId = configuracao.Id;
                incoming.ConfiguracaoCobranca = null;
                incoming.DataCadastro = agora;
                configuracao.ConfiguracaoAgendamento = incoming;
                _context.Add(incoming);
                return;
            }

            // Alterar a janela de execução invalida a próxima data já calculada pelo job.
            if (AgendamentoFoiReprogramado(existente, incoming))
                existente.ProximaExecucao = null;

            existente.TipoJob = incoming.TipoJob;
            existente.ModalidadeCobranca = incoming.ModalidadeCobranca;
            existente.Intervalo = incoming.Intervalo;
            existente.DiaSemana = incoming.DiaSemana;
            existente.DiaMes = incoming.DiaMes;
            existente.HoraExecucao = incoming.HoraExecucao;
            existente.Ativo = incoming.Ativo;
            existente.DataAtualizacao = agora;
        }

        private static bool AgendamentoFoiReprogramado(
            ConfiguracaoAgendamento existente,
            ConfiguracaoAgendamento incoming)
        {
            return existente.ModalidadeCobranca != incoming.ModalidadeCobranca
                || existente.Intervalo != incoming.Intervalo
                || existente.DiaSemana != incoming.DiaSemana
                || existente.DiaMes != incoming.DiaMes
                || existente.HoraExecucao != incoming.HoraExecucao;
        }
    }
}
