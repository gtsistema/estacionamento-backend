using Estac.Api.Controllers.Base.Claim;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class TransportadoraController : ControllerBase
    {
        public readonly ITransportadoraService _services;

        public TransportadoraController(ITransportadoraService _services)
        {
            this._services = _services;
        }

        [PermissionAuthorize(Permission.Transportadora.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] TransportadoraFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(Permission.Transportadora.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(Permission.Transportadora.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] TransportadoraPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(Permission.Transportadora.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] TransportadoraPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(Permission.Transportadora.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }
    }
}
