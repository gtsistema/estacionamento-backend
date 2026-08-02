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
    public class ConfiguracaoCobrancaController : ControllerBase
    {
        private readonly IConfiguracaoCobrancaService _services;

        public ConfiguracaoCobrancaController(IConfiguracaoCobrancaService services)
        {
            _services = services;
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] ConfiguracaoCobrancaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] ConfiguracaoCobrancaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] ConfiguracaoCobrancaPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.ConfiguracaoCobranca.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            return await _services.Excluir(id);
        }
    }
}
