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
                .Include(x => x.ConfiguracoesAgendamento)
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
                    ValorEstadia = x.ValorEstadia,
                    EmailFinanceiro = x.EmailFinanceiro,
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
                var incomingAgendamentos = item.ConfiguracoesAgendamento?
                    .Where(a => a != null)
                    .ToList() ?? new List<ConfiguracaoAgendamento>();

                item.ConfiguracoesAgendamento = null;

                var result = await _dataset
                    .Include(x => x.ConfiguracoesAgendamento)
                    .SingleOrDefaultAsync(p => p.Id == item.Id);

                if (result == null)
                    return null;

                item.DataCriacao = result.DataCriacao;
                item.DataAtualizacao = DateTime.Now;
                _context.Entry(result).CurrentValues.SetValues(item);

                SincronizarAgendamentos(result, incomingAgendamentos);
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
                    .Include(x => x.ConfiguracoesAgendamento)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (configuracao is null)
                    return;

                if (configuracao.ConfiguracoesAgendamento?.Count > 0)
                    _context.RemoveRange(configuracao.ConfiguracoesAgendamento);

                _context.Remove(configuracao);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private void SincronizarAgendamentos(ConfiguracaoCobranca existente, List<ConfiguracaoAgendamento> incoming)
        {
            existente.ConfiguracoesAgendamento ??= new List<ConfiguracaoAgendamento>();

            var existentes = existente.ConfiguracoesAgendamento.ToList();
            var agora = DateTime.Now;

            // Regra: no máximo 1 agendamento por ConfiguracaoCobranca (TipoJob GerarFaturamento).
            var item = incoming.FirstOrDefault();
            if (item is null)
            {
                foreach (var remover in existentes)
                {
                    existente.ConfiguracoesAgendamento.Remove(remover);
                    _context.Remove(remover);
                }
                return;
            }

            if (item.Intervalo <= 0)
                item.Intervalo = 1;

            item.TipoJob = TipoJob.GerarFaturamento;

            var alvo = existentes.FirstOrDefault(e => e.Id == item.Id && item.Id != Guid.Empty)
                ?? existentes.FirstOrDefault(e => e.TipoJob == TipoJob.GerarFaturamento)
                ?? existentes.FirstOrDefault();

            foreach (var remover in existentes.Where(e => alvo == null || e.Id != alvo.Id).ToList())
            {
                existente.ConfiguracoesAgendamento.Remove(remover);
                _context.Remove(remover);
            }

            if (alvo != null)
            {
                alvo.TipoJob = item.TipoJob;
                alvo.Periodicidade = item.Periodicidade;
                alvo.Intervalo = item.Intervalo;
                alvo.DiaSemana = item.DiaSemana;
                alvo.DiaMes = item.DiaMes;
                alvo.HoraExecucao = item.HoraExecucao;
                alvo.Ativo = item.Ativo;
                alvo.DataAtualizacao = agora;
                return;
            }

            existente.ConfiguracoesAgendamento.Add(new ConfiguracaoAgendamento
            {
                Id = item.Id != Guid.Empty ? item.Id : Guid.NewGuid(),
                ConfiguracaoCobrancaId = existente.Id,
                TipoJob = item.TipoJob,
                Periodicidade = item.Periodicidade,
                Intervalo = item.Intervalo,
                DiaSemana = item.DiaSemana,
                DiaMes = item.DiaMes,
                HoraExecucao = item.HoraExecucao,
                Ativo = item.Ativo,
                DataCadastro = agora
            });
        }
    }
}
