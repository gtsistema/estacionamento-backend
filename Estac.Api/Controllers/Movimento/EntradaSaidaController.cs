using Estac.Domain.Input.Movimento.Entrada;
using Estac.Domain.Input.Movimento.EntradaSaida;
using Estac.Domain.Interface.Services;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Movimento
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EntradaSaidaController : ControllerBase
    {
        private readonly IEntradaSaidaService _services;

        public EntradaSaidaController(IEntradaSaidaService services)
        {
            _services = services;
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] EntradaSaidaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Visualizar)]
        [HttpGet("buscar-por-placa/{placa}")]
        public async Task<ActionResult> ObterPorPlaca(string placa)
        {
            return await _services.ObterPorPlaca(placa);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] EntradaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Alterar)]
        [HttpPost("saida")]
        public async Task<ActionResult> Saida([FromBody] EntradaSaidaPlacaInput input)
        {
            return await _services.Saida(input);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Alterar)]
        [HttpPatch("{id}/suspender-permanencia")]
        public async Task<ActionResult> SuspenderPermanencia(int id, [FromBody] EntradaSaidaPermanenciaInput input)
        {
            return await _services.SuspenderPermanencia(id, input);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Alterar)]
        [HttpPatch("{id}/finalizar-permanencia")]
        public async Task<ActionResult> FinalizarPermanencia(int id, [FromQuery] DateTime? dataHoraSaida)
        {
            return await _services.FinalizarPermanencia(id, dataHoraSaida);
        }

        [PermissionAuthorize(PermissionAcess.EntradaSaida.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }
    }
}