using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MotoristaController : ControllerBase
    {
        public readonly IMotoristaService _services;

        public MotoristaController(IMotoristaService _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] MotoristaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] MotoristaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] MotoristaPutInput input)
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
