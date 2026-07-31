using Estac.Domain.Extensions;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class ConfiguracaoAgendamentoRepositories : IConfiguracaoAgendamentoRepositories
    {
        private readonly DbSet<ConfiguracaoAgendamento> _dataset;

        public ConfiguracaoAgendamentoRepositories(GtsContext context)
        {
            _dataset = context.Set<ConfiguracaoAgendamento>();
        }

        public async Task<ConfiguracaoAgendamento> SelecionarPorIdCompleto(Guid id)
        {
            return await _dataset
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ConfiguracaoAgendamento> SelecionarPorIdParaAtualizacao(Guid id)
        {
            return await _dataset.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AtualizarExecucao(ConfiguracaoAgendamento agendamento)
        {
            _dataset.Update(agendamento);
            await Task.CompletedTask;
        }

        public async Task<PagedResult<ConfiguracaoAgendamentoOutput>> Paginar(ConfiguracaoAgendamentoFilterInput input)
        {
            var query = _dataset
                .AsNoTracking()
                .AsQueryable();

            if (input.ConfiguracaoCobrancaId.HasValue)
                query = query.Where(x => x.ConfiguracaoCobrancaId == input.ConfiguracaoCobrancaId.Value);

            if (input.TipoJob.HasValue)
                query = query.Where(x => x.TipoJob == input.TipoJob.Value);

            if (input.ModalidadeCobranca.HasValue)
                query = query.Where(x => x.ModalidadeCobranca == input.ModalidadeCobranca.Value);

            if (input.Ativo.HasValue)
                query = query.Where(x => x.Ativo == input.Ativo.Value);

            if (input.DataInicial.HasValue)
                query = query.Where(x => x.DataCadastro.Date >= input.DataInicial.Value.Date);

            if (input.DataFinal.HasValue)
                query = query.Where(x => x.DataCadastro.Date <= input.DataFinal.Value.Date);

            return await query
                .OrderByDescending(x => x.DataCadastro)
                .Select(x => new ConfiguracaoAgendamentoOutput
                {
                    Id = x.Id,
                    ConfiguracaoCobrancaId = x.ConfiguracaoCobrancaId,
                    TipoJob = x.TipoJob,
                    ModalidadeCobranca = x.ModalidadeCobranca,
                    Intervalo = x.Intervalo,
                    DiaSemana = x.DiaSemana,
                    DiaMes = x.DiaMes,
                    HoraExecucao = x.HoraExecucao,
                    UltimaExecucao = x.UltimaExecucao,
                    ProximaExecucao = x.ProximaExecucao,
                    Ativo = x.Ativo,
                    DataCadastro = x.DataCadastro,
                    DataAtualizacao = x.DataAtualizacao
                })
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }

        public async Task<ConfiguracaoAgendamento> SelecionarPorConfiguracaoCobranca(int id)
        {
            return await _dataset
                .SingleOrDefaultAsync(x => x.ConfiguracaoCobrancaId == id);
        }
    }
}
