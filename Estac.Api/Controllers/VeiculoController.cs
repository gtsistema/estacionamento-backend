using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Services;
using Estac.Domain.Output.Veiculo;
using Estac.Service;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VeiculoController : ControllerBase
    {
        public readonly IVeiculoService _services;

        public VeiculoController(IVeiculoService _services)
        {
             this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] VeiculoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Search(int id)
        {
            return await _services.ObterPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VeiculoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] VeiculoPutInput input)
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
