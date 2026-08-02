using AutoMapper;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Output;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Services.Faturamento;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class ConfiguracaoAgendamentoService : ServiceResult<ConfiguracaoAgendamentoOutput>, IConfiguracaoAgendamentoService
    {
        private readonly IConfiguracaoAgendamentoRepositories _repositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConfiguracaoAgendamentoService(
            IErrorServices errorServices,
            IConfiguracaoAgendamentoRepositories repositories,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(errorServices)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var result = await _repositories.SelecionarPorIdCompleto(id);
            if (result is null)
                return await RetornNo(false, "Configuração de agendamento não localizada na base de dados.", statusCode: 404);

            return await RetornOk(_mapper.Map<ConfiguracaoAgendamentoOutput>(result));
        }

        public async Task<ActionResult> Buscar(ConfiguracaoAgendamentoFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);
            return await RetornOk(result);
        }

        public async Task<ActionResult> Alterar(ConfiguracaoAgendamentoPutInput input)
        {
            var validations = ConfiguracaoAgendamentoPutInput.Validar(input);
            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            var entity = await _repositories.SelecionarPorIdParaAtualizacao(input.Id);
            if (entity is null)
                return await RetornNo(false, "Configuração de agendamento não localizada na base de dados.", statusCode: 404);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                entity.UltimaExecucao = input.UltimaExecucao;
                entity.ProximaExecucao = input.ProximaExecucao
                    ?? PeriodoFaturamentoCalculator.CalcularProximaExecucao(
                        entity.ModalidadeCobranca,
                        entity.Intervalo,
                        entity.DiaSemana,
                        entity.DiaMes,
                        entity.HoraExecucao,
                        input.UltimaExecucao);
                entity.DataAtualizacao = DateTime.Now;

                await _repositories.AtualizarExecucao(entity);
                await _unitOfWork.CommitAsync();

                var completo = await _repositories.SelecionarPorIdCompleto(entity.Id);
                return await RetornOk(_mapper.Map<ConfiguracaoAgendamentoOutput>(completo));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }
    }
}
