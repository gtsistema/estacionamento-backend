using Estac.Api.Controllers.Base;
using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Controllers.Base.Permission;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MotoristaController : ControllerBase
    {
        public readonly IMotoristaService _services;

        public MotoristaController(IMotoristaService _services)
        {
            this._services = _services;
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] MotoristaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Visualizar)]

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] MotoristaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] MotoristaPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }
    }
}
