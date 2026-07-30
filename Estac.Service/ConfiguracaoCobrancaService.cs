using AutoMapper;
using Estac.Domain.Factories;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class ConfiguracaoCobrancaService : ServiceResult<ConfiguracaoCobrancaOutput>, IConfiguracaoCobrancaService
    {
        private readonly IConfiguracaoCobrancaRepositories _repositories;
        private readonly ITransportadoraRepositories _transportadoraRepositories;
        private readonly IEstacionamentoRepositories _estacionamentoRepositories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ConfiguracaoCobrancaService(
            IErrorServices errorServices,
            IConfiguracaoCobrancaRepositories repositories,
            ITransportadoraRepositories transportadoraRepositories,
            IEstacionamentoRepositories estacionamentoRepositories,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(errorServices)
        {
            _repositories = repositories;
            _transportadoraRepositories = transportadoraRepositories;
            _estacionamentoRepositories = estacionamentoRepositories;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.SelecionarPorIdCompleto(id);
            if (result is null)
                return await RetornNo(false, "Configuração de cobrança não localizada na base de dados.", statusCode: 404);

            return await RetornOk(_mapper.Map<ConfiguracaoCobrancaOutput>(result));
        }

        public async Task<ActionResult> Buscar(ConfiguracaoCobrancaFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);
            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(ConfiguracaoCobrancaPostInput input)
        {
            var validations = ConfiguracaoCobrancaPostInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            if (!await _transportadoraRepositories.Existe(input.TransportadoraId))
                return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

            if (!await _estacionamentoRepositories.Existe(input.EstacionamentoId))
                return await RetornNo(false, "Estacionamento não localizado na base de dados.", statusCode: 404);

            if (await _repositories.ExistePorTransportadoraEstacionamentoAsync(input.TransportadoraId, input.EstacionamentoId))
                return await RetornNo(false, "Já existe uma configuração de cobrança para esta transportadora e estacionamento.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<ConfiguracaoCobranca>(input);
                ValoresPadrao(entity);
                NormalizarAgendamentoGerarFaturamento(entity);

                await _repositories.Gravar(entity);
                await _unitOfWork.CommitAsync();

                var completo = await _repositories.SelecionarPorIdCompleto(entity.Id);
                return await RetornOk(_mapper.Map<ConfiguracaoCobrancaOutput>(completo));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(ConfiguracaoCobrancaPutInput input)
        {
            var validations = ConfiguracaoCobrancaPutInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            if (!await _repositories.Existe(input.Id))
                return await RetornNo(false, "Configuração de cobrança não localizada na base de dados.", statusCode: 404);

            if (!await _transportadoraRepositories.Existe(input.TransportadoraId))
                return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

            if (!await _estacionamentoRepositories.Existe(input.EstacionamentoId))
                return await RetornNo(false, "Estacionamento não localizado na base de dados.", statusCode: 404);

            if (await _repositories.ExistePorTransportadoraEstacionamentoAsync(input.TransportadoraId, input.EstacionamentoId, input.Id))
                return await RetornNo(false, "Já existe uma configuração de cobrança para esta transportadora e estacionamento.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<ConfiguracaoCobranca>(input);
                ValoresPadrao(entity);
                NormalizarAgendamentoGerarFaturamento(entity);

                await _repositories.Alterar(entity);
                await _unitOfWork.CommitAsync();

                var completo = await _repositories.SelecionarPorIdCompleto(input.Id);
                return await RetornOk(_mapper.Map<ConfiguracaoCobrancaOutput>(completo));
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
                return await RetornNo(false, "Configuração de cobrança não localizada na base de dados.", statusCode: 404);

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

        private static void ValoresPadrao(ConfiguracaoCobranca entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Descricao))
                entity.Descricao = $"Cobrança {entity.TransportadoraId}/{entity.EstacionamentoId}";

            DescartarValoresInativos(entity);
        }

        /// <summary>
        /// Zera o que depende de uma opção desligada, evitando resíduo de configurações anteriores
        /// que voltaria ao cliente na próxima leitura.
        /// </summary>
        private static void DescartarValoresInativos(ConfiguracaoCobranca entity)
        {
            if (entity.ModalidadeCobranca != ModalidadeCobranca.Personalizado)
                entity.DataCobranca = null;

            if (!entity.CobrarLavagem)
                entity.ValorLavagem = null;

            if (!entity.CobrarPernoite)
                entity.ValorPernoite = null;

            if (!entity.CobrarServicosExtras)
                entity.ValorServicosExtras = null;

            if (!entity.ConsiderarBeneficioAbastecimento)
                entity.ValorBeneficioAbastecimento = null;
        }

        /// <summary>
        /// Só monta ConfiguracaoAgendamento quando GerarFaturaAutomaticamente = true.
        /// Ao desativar a geração, o repositório preserva e inativa o agendamento existente.
        /// </summary>
        private static void NormalizarAgendamentoGerarFaturamento(ConfiguracaoCobranca entity)
        {
            entity.ConfiguracaoAgendamento = ConfiguracaoAgendamentoFactory.Criar(entity);
        }
    }
}
