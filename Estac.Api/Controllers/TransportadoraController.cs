using Estac.Domain.Input.Transportadora;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransportadoraController : ControllerBase
    {
        public readonly ITransportadoraService _services;

        public TransportadoraController(ITransportadoraService _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] TransportadoraFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] TransportadoraPostInput input)
        {
            return await _services.Gravar(input);
        }

        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] TransportadoraPutInput input)
        {
            return await _services.Alterar(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }
    }
}
