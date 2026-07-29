using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Services;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Financeiro
{
    [Authorize]
    [ApiController]
    [Route("api/financeiro/[controller]")]
    public class ConfiguracaoAgendamentoController : ControllerBase
    {
        private readonly IConfiguracaoAgendamentoService _services;

        public ConfiguracaoAgendamentoController(IConfiguracaoAgendamentoService services)
        {
            _services = services;
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] ConfiguracaoAgendamentoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return await _services.ObterPorId(id);
        }
    }
}
