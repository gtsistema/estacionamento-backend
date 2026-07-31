using AutoMapper;
using Estac.Domain.Input.Fatura;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Fatura;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class FaturaService : ServiceResult<FaturaOutput>, IFaturaService
    {
        private readonly IFaturaRepositories _repositories;
        private readonly ITransportadoraRepositories _transportadoraRepositories;
        private readonly IEstacionamentoRepositories _estacionamentoRepositories;
        private readonly IConfiguracaoCobrancaRepositories _configuracaoCobrancaRepositories;
        private readonly IEntradaSaidaRepositories _entradaSaidaRepositories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FaturaService(
            IErrorServices errorServices,
            IFaturaRepositories repositories,
            ITransportadoraRepositories transportadoraRepositories,
            IEstacionamentoRepositories estacionamentoRepositories,
            IConfiguracaoCobrancaRepositories configuracaoCobrancaRepositories,
            IEntradaSaidaRepositories entradaSaidaRepositories,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(errorServices)
        {
            _repositories = repositories;
            _transportadoraRepositories = transportadoraRepositories;
            _estacionamentoRepositories = estacionamentoRepositories;
            _configuracaoCobrancaRepositories = configuracaoCobrancaRepositories;
            _entradaSaidaRepositories = entradaSaidaRepositories;
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

        public async Task<ActionResult> Gravar(FaturaPostInput input)
        {
            var validations = FaturaPostInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            var referenciaInvalida = await ValidarReferenciasAsync(input);
            if (referenciaInvalida != null)
                return referenciaInvalida;

            if (!string.IsNullOrWhiteSpace(input.Numero)
                && await _repositories.ExisteNumeroAsync(input.Numero.Trim()))
                return await RetornNo(false, "Já existe uma fatura com este número.");

            if (input.Itens is { Count: > 0 })
            {
                var jaFaturados = await _repositories.ObterEntradaSaidaJaFaturadas(
                    input.Itens.Select(x => x.EntradaSaidaId));

                if (jaFaturados.Count > 0)
                {
                    return await RetornNo(
                        false,
                        $"Existem movimentações já faturadas: {string.Join(", ", jaFaturados)}.");
                }
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<Fatura>(input);
                ValoresPadrao(entity);
                MapearItens(entity, input.Itens);

                if (string.IsNullOrWhiteSpace(entity.Numero))
                    entity.Numero = GerarNumeroProvisorio(entity.DataEmissao);

                await _repositories.Gravar(entity);

                if (input.Itens is { Count: > 0 })
                {
                    await _entradaSaidaRepositories.MarcarComoFaturadasAsync(
                        input.Itens.Select(x => x.EntradaSaidaId),
                        DateTime.Now);
                }

                await _unitOfWork.CommitAsync();

                return await RetornOk(await _repositories.SelecionarPorIdCompleto(entity.Id));
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

            var referenciaInvalida = await ValidarReferenciasAsync(input);
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

        private async Task<ActionResult> ValidarReferenciasAsync(FaturaPostInput input)
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

        private static void MapearItens(Fatura entity, List<FaturaItemPostInput> itens)
        {
            entity.Itens ??= new List<FaturaItem>();
            entity.Itens.Clear();

            if (itens is null || itens.Count == 0)
                return;

            foreach (var item in itens)
            {
                entity.Itens.Add(new FaturaItem
                {
                    EntradaSaidaId = item.EntradaSaidaId,
                    Placa = item.Placa,
                    DataHoraEntrada = item.DataHoraEntrada,
                    DataHoraSaida = item.DataHoraSaida,
                    TempoPermanenciaMinutos = item.TempoPermanenciaMinutos,
                    ValorEstacionamento = item.ValorEstacionamento,
                    ValorLavagem = item.ValorLavagem,
                    ValorPernoite = item.ValorPernoite,
                    ValorServicosExtras = item.ValorServicosExtras,
                    ValorBeneficioAbastecimento = item.ValorBeneficioAbastecimento,
                    ValorTotal = item.ValorTotal,
                    Descricao = string.IsNullOrWhiteSpace(item.Descricao)
                        ? $"Movimento {item.EntradaSaidaId}"
                        : item.Descricao,
                    DataCriacao = DateTime.Now
                });
            }
        }

        private static string GerarNumeroProvisorio(DateTime dataEmissao) =>
            $"FAT-{dataEmissao:yyyyMM}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
    }
}
