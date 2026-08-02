using AutoMapper;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Output;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class ConfiguracaoAgendamentoService : ServiceResult<ConfiguracaoAgendamentoOutput>, IConfiguracaoAgendamentoService
    {
        private readonly IConfiguracaoAgendamentoRepositories _repositories;
        private readonly IMapper _mapper;

        public ConfiguracaoAgendamentoService(
            IErrorServices errorServices,
            IConfiguracaoAgendamentoRepositories repositories,
            IMapper mapper) : base(errorServices)
        {
            _repositories = repositories;
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
    }
}
