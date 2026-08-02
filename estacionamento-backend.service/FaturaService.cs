using AutoMapper;
using Estac.Domain.Input.Fatura;
using Estac.Domain.Input.Faturamento;
using Estac.Domain.Interface.Integration;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Fatura;
using Estac.Domain.Output.Faturamento;
using Estac.Domain.Services.Faturamento;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class FaturaService : ServiceResult<FaturaOutput>, IFaturaService
    {
        private const string TipoRelatorioFatura = "fatura";
        private const int TamanhoLoteMovimentos = 500;

        private readonly IFaturaRepositories _repositories;
        private readonly ITransportadoraRepositories _transportadoraRepositories;
        private readonly IEstacionamentoRepositories _estacionamentoRepositories;
        private readonly IConfiguracaoCobrancaRepositories _configuracaoCobrancaRepositories;
        private readonly IEntradaSaidaRepositories _entradaSaidaRepositories;
        private readonly IEstacionamentoReportClient _reportClient;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FaturaService(
            IErrorServices errorServices,
            IFaturaRepositories repositories,
            ITransportadoraRepositories transportadoraRepositories,
            IEstacionamentoRepositories estacionamentoRepositories,
            IConfiguracaoCobrancaRepositories configuracaoCobrancaRepositories,
            IEntradaSaidaRepositories entradaSaidaRepositories,
            IEstacionamentoReportClient reportClient,
            ICurrentUser currentUser,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(errorServices)
        {
            _repositories = repositories;
            _transportadoraRepositories = transportadoraRepositories;
            _estacionamentoRepositories = estacionamentoRepositories;
            _configuracaoCobrancaRepositories = configuracaoCobrancaRepositories;
            _entradaSaidaRepositories = entradaSaidaRepositories;
            _reportClient = reportClient;
            _currentUser = currentUser;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.SelecionarPorIdCompleto(id);
            if (result is null)
                return await RetornNo(false, "Fatura não localizada na base de dados.", statusCode: 404);

            return await RetornOk(_mapper.Map<FaturaOutput>(result));
        }

        public async Task<ActionResult> Buscar(FaturaFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);
            return await RetornOk(result);
        }

        public async Task<ActionResult> ObterVisaoGeral(FaturaFilterInput filter)
        {
            var result = await _repositories.ObterVisaoGeral(filter);
            return await RetornOk(result);
        }

        /// <summary>
        /// Gera fatura para o par transportadora/estacionamento.
        /// EstacionamentoId nulo → usa EmpresaId do usuário logado.
        /// </summary>
        public async Task<ActionResult> Gravar(FaturaPostInput input)
        {
            var validations = FaturaPostInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            var estacionamentoId = ResolverEstacionamentoId(input.EstacionamentoId);
            if (estacionamentoId <= 0)
                return await RetornNo(false, "Estacionamento é obrigatório (informe no body ou use usuário com EmpresaId).");

            if (!await _transportadoraRepositories.Existe(input.TransportadoraId))
                return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

            if (!await _estacionamentoRepositories.Existe(estacionamentoId))
                return await RetornNo(false, "Estacionamento não localizado na base de dados.", statusCode: 404);

            var agendamento = await _repositories.SelecionarAgendamentoParaGeracao(
                input.TransportadoraId,
                estacionamentoId);

            if (agendamento is null || agendamento.Cobranca is null)
            {
                return await RetornNo(
                    false,
                    "Não há configuração/agendamento ativo de faturamento automático para esta transportadora e estacionamento.",
                    statusCode: 404);
            }

            var agora = DateTime.Now;
            var (periodoInicio, periodoFim) = PeriodoFaturamentoCalculator.CalcularPeriodo(
                agendamento.ModalidadeCobranca,
                agendamento.Intervalo,
                agendamento.UltimaExecucao,
                agendamento.ProximaExecucao,
                agendamento.UltimoPeriodoFaturado,
                agora);

            if (periodoFim <= periodoInicio)
                return await RetornNo(false, "Período de competência inválido para geração da fatura.");

            var movimentos = await CarregarTodosMovimentosAsync(
                estacionamentoId,
                input.TransportadoraId,
                periodoInicio,
                periodoFim);

            if (movimentos.Count == 0)
            {
                return await RetornNo(
                    false,
                    "Não há movimentos faturáveis no período para gerar a fatura.");
            }

            var entity = FaturaMontagem.Montar(agendamento, movimentos, agora, periodoInicio, periodoFim);
            if (entity.ValorTotal <= 0)
                return await RetornNo(false, "Valor total da fatura ficou zerado — geração abortada.");

            var jaFaturados = await _repositories.ObterEntradaSaidaJaFaturadas(
                entity.Itens.Select(x => x.EntradaSaidaId));
            if (jaFaturados.Count > 0)
            {
                return await RetornNo(
                    false,
                    $"Existem movimentações já faturadas: {string.Join(", ", jaFaturados)}.");
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _repositories.Gravar(entity);

                await _entradaSaidaRepositories.MarcarComoFaturadasAsync(
                    entity.Itens.Select(x => x.EntradaSaidaId),
                    agora);

                await _unitOfWork.CommitAsync();

                var completo = await _repositories.SelecionarPorIdCompleto(entity.Id);
                return await RetornOk(_mapper.Map<FaturaOutput>(completo));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(FaturaPutInput input)
        {
            var validations = FaturaPutInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            if (!await _repositories.Existe(input.Id))
                return await RetornNo(false, "Fatura não localizada na base de dados.", statusCode: 404);

            var referenciaInvalida = await ValidarReferenciasPutAsync(input);
            if (referenciaInvalida != null)
                return referenciaInvalida;

            if (!string.IsNullOrWhiteSpace(input.Numero)
                && await _repositories.ExisteNumeroAsync(input.Numero.Trim(), input.Id))
                return await RetornNo(false, "Já existe uma fatura com este número.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<Fatura>(input);
                ValoresPadrao(entity);

                await _repositories.Alterar(entity);
                await _unitOfWork.CommitAsync();

                return await RetornOk(await _repositories.SelecionarPorIdCompleto(input.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            if (!await _repositories.Existe(id))
                return await RetornNo(false, "Fatura não localizada na base de dados.", statusCode: 404);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _repositories.Remove(id);
                await _unitOfWork.CommitAsync();

                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> GerarRelatorio(int id, FormatoRelatorio formato, CancellationToken cancellationToken = default)
        {
            if (!await _repositories.Existe(id))
                return await RetornNo(false, "Fatura não localizada na base de dados.", statusCode: 404);

            var resultado = await _reportClient.GerarRelatorioAsync(
                TipoRelatorioFatura,
                id,
                formato,
                cancellationToken);

            if (!resultado.Success)
            {
                var statusCode = (int)resultado.StatusCode;
                if (statusCode < 400)
                    statusCode = 502;

                return await RetornNo(
                    false,
                    string.IsNullOrWhiteSpace(resultado.ErrorBody)
                        ? "Não foi possível gerar o relatório da fatura."
                        : resultado.ErrorBody,
                    statusCode: statusCode);
            }

            var extensaoPadrao = formato == FormatoRelatorio.Excel ? "xlsx" : "pdf";

            return new FileContentResult(resultado.Content, resultado.ContentType)
            {
                FileDownloadName = string.IsNullOrWhiteSpace(resultado.FileName)
                    ? $"fatura-{id}.{extensaoPadrao}"
                    : resultado.FileName
            };
        }

        public Task<ActionResult> GerarExcel(int id, CancellationToken cancellationToken = default) =>
            GerarRelatorio(id, FormatoRelatorio.Excel, cancellationToken);

        private int ResolverEstacionamentoId(int? estacionamentoIdInformado)
        {
            if (estacionamentoIdInformado.HasValue && estacionamentoIdInformado.Value > 0)
                return estacionamentoIdInformado.Value;

            return _currentUser.EmpresaId;
        }

        private async Task<IList<EntradaSaidaFaturavelOutput>> CarregarTodosMovimentosAsync(
            int estacionamentoId,
            int transportadoraId,
            DateTime periodoInicio,
            DateTime periodoFim)
        {
            var todos = new List<EntradaSaidaFaturavelOutput>();
            int? cursor = null;
            bool possuiMais;

            do
            {
                var lote = await _repositories.SelecionarMovimentosFaturaveis(
                    new EntradaSaidaFaturavelFilterInput
                    {
                        EstacionamentoId = estacionamentoId,
                        TransportadoraId = transportadoraId,
                        PeriodoInicio = periodoInicio,
                        PeriodoFim = periodoFim,
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

        private async Task<ActionResult> ValidarReferenciasPutAsync(FaturaPutInput input)
        {
            if (!await _transportadoraRepositories.Existe(input.TransportadoraId))
                return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

            if (!await _estacionamentoRepositories.Existe(input.EstacionamentoId))
                return await RetornNo(false, "Estacionamento não localizado na base de dados.", statusCode: 404);

            if (input.ConfiguracaoCobrancaId.HasValue
                && !await _configuracaoCobrancaRepositories.Existe(input.ConfiguracaoCobrancaId.Value))
                return await RetornNo(false, "Configuração de cobrança não localizada na base de dados.", statusCode: 404);

            return null;
        }

        private static void ValoresPadrao(Fatura entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.Numero))
                entity.Numero = entity.Numero.Trim();

            if (string.IsNullOrWhiteSpace(entity.Descricao))
                entity.Descricao = string.IsNullOrWhiteSpace(entity.Numero)
                    ? $"Fatura {entity.TransportadoraId}/{entity.EstacionamentoId}"
                    : $"Fatura {entity.Numero}";

            if (entity.Status == StatusFatura.Pago && entity.ValorRecebido <= 0)
                entity.ValorRecebido = entity.ValorTotal;

            if (entity.Status == default)
                entity.Status = StatusFatura.AguardandoEnvio;
        }
    }
}
