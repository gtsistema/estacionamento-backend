using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Services;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Permission;
using Estac.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        public readonly IVeiculoService _services;

        public VeiculoController(IVeiculoService _services)
        {
             this._services = _services;
        }

        [PermissionAuthorize(PermissionAcess.Veiculo.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] VeiculoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.Veiculo.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Veiculo.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] VeiculoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Veiculo.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] VeiculoPutInput input)
        {
            return await _services.Alterar(input);
        }
        
        [PermissionAuthorize(PermissionAcess.Veiculo.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }

        [PermissionAuthorize(PermissionAcess.Motorista.Visualizar)]
        [HttpGet("por-placa/{placa}")]
        public async Task<ActionResult> ObterVinculosPorPlaca(string placa)
        {
            return await _services.ObterVinculosPorPlaca(placa);
        }
    }
}
