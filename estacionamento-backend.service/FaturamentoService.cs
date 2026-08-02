using Estac.Domain.Input.Faturamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Output;
using Estac.Domain.Output.Faturamento;
using Estac.Domain.Services.Faturamento;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class FaturamentoService : ServiceResult<object>, IFaturamentoService
    {
        private const int TamanhoLoteMovimentos = 500;

        private readonly IFaturaRepositories _faturaRepositories;
        private readonly IConfiguracaoAgendamentoRepositories _agendamentoRepositories;
        private readonly IEntradaSaidaRepositories _entradaSaidaRepositories;
        private readonly IUnitOfWork _unitOfWork;

        public FaturamentoService(
            IErrorServices errorServices,
            IFaturaRepositories faturaRepositories,
            IConfiguracaoAgendamentoRepositories agendamentoRepositories,
            IEntradaSaidaRepositories entradaSaidaRepositories,
            IUnitOfWork unitOfWork) : base(errorServices)
        {
            _faturaRepositories = faturaRepositories;
            _agendamentoRepositories = agendamentoRepositories;
            _entradaSaidaRepositories = entradaSaidaRepositories;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> ObterElegiveis(DateTime? referencia = null)
        {
            var agora = referencia ?? DateTime.Now;
            var agendamentos = await _faturaRepositories.SelecionarAgendamentosPendentes(agora);
            var resultado = new List<ConfiguracaoFaturavelOutput>();

            foreach (var agendamento in agendamentos)
            {
                if (!ValidarConfiguracao(agendamento))
                    continue;

                if (!PeriodoFaturamentoCalculator.AtendeRegrasAgendamento(
                        agendamento.ModalidadeCobranca,
                        agendamento.DiaSemana,
                        agendamento.DiaMes,
                        agendamento.HoraExecucao,
                        agendamento.ProximaExecucao,
                        agora))
                    continue;

                var (periodoInicio, periodoFim) = PeriodoFaturamentoCalculator.CalcularPeriodo(
                    agendamento.ModalidadeCobranca,
                    agendamento.Intervalo,
                    agendamento.UltimaExecucao,
                    agendamento.ProximaExecucao,
                    agendamento.UltimoPeriodoFaturado,
                    agora);

                if (periodoFim <= periodoInicio)
                    continue;

                var movimentos = await CarregarTodosMovimentosAsync(
                    agendamento.EstacionamentoId,
                    agendamento.TransportadoraId);

                resultado.Add(new ConfiguracaoFaturavelOutput
                {
                    ConfiguracaoAgendamentoId = agendamento.ConfiguracaoAgendamentoId,
                    ConfiguracaoCobrancaId = agendamento.ConfiguracaoCobrancaId,
                    TransportadoraId = agendamento.TransportadoraId,
                    TransportadoraNome = agendamento.TransportadoraNome,
                    EstacionamentoId = agendamento.EstacionamentoId,
                    EstacionamentoNome = agendamento.EstacionamentoNome,
                    ModalidadeCobranca = agendamento.ModalidadeCobranca,
                    Intervalo = agendamento.Intervalo,
                    DiaSemana = agendamento.DiaSemana,
                    DiaMes = agendamento.DiaMes,
                    HoraExecucao = agendamento.HoraExecucao,
                    UltimaExecucao = agendamento.UltimaExecucao,
                    ProximaExecucao = agendamento.ProximaExecucao,
                    UltimoPeriodoFaturado = agendamento.UltimoPeriodoFaturado,
                    Cobranca = agendamento.Cobranca,
                    PeriodoInicio = periodoInicio,
                    PeriodoFim = periodoFim,
                    Movimentos = movimentos
                });
            }

            return await RetornOk(resultado);
        }

        public async Task<ActionResult> RegistrarExecucao(RegistrarExecucaoAgendamentoInput input)
        {
            if (input is null || input.ConfiguracaoAgendamentoId == Guid.Empty)
                return await RetornNo(false, "Identificador do agendamento é obrigatório.");

            if (input.ProximaExecucao <= input.UltimaExecucao)
                return await RetornNo(false, "Próxima execução deve ser posterior à última execução.");

            var agendamento = await _agendamentoRepositories.SelecionarPorIdParaAtualizacao(input.ConfiguracaoAgendamentoId);
            if (agendamento is null)
                return await RetornNo(false, "Agendamento não localizado na base de dados.", statusCode: 404);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                agendamento.UltimaExecucao = input.UltimaExecucao;
                agendamento.ProximaExecucao = input.ProximaExecucao;
                agendamento.DataAtualizacao = DateTime.Now;
                await _agendamentoRepositories.AtualizarExecucao(agendamento);

                if (input.EntradaSaidaIds is { Count: > 0 })
                {
                    await _entradaSaidaRepositories.MarcarComoFaturadasAsync(
                        input.EntradaSaidaIds,
                        input.UltimaExecucao);
                }

                await _unitOfWork.CommitAsync();
                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        private static bool ValidarConfiguracao(AgendamentoFaturamentoOutput agendamento)
        {
            if (agendamento.ConfiguracaoCobrancaId <= 0)
                return false;

            if (agendamento.TransportadoraId <= 0)
                return false;

            if (agendamento.EstacionamentoId <= 0)
                return false;

            if (agendamento.Cobranca is null)
                return false;

            return Enum.IsDefined(agendamento.ModalidadeCobranca);
        }

        private async Task<IList<EntradaSaidaFaturavelOutput>> CarregarTodosMovimentosAsync(
            int estacionamentoId,
            int transportadoraId)
        {
            var todos = new List<EntradaSaidaFaturavelOutput>();
            int? cursor = null;
            bool possuiMais;

            do
            {
                var lote = await _faturaRepositories.SelecionarMovimentosFaturaveis(
                    new EntradaSaidaFaturavelFilterInput
                    {
                        EstacionamentoId = estacionamentoId,
                        TransportadoraId = transportadoraId,
                        UltimoId = cursor,
                        Tamanho = TamanhoLoteMovimentos
                    });

                if (lote.Itens is { Count: > 0 })
                    todos.AddRange(lote.Itens);

                possuiMais = lote.PossuiMais;
                cursor = lote.ProximoCursor;
            } while (possuiMais && cursor.HasValue);

            return todos;
        }
    }
}
