using Estac.Domain.Input.Fatura;
using Estac.Domain.Interface.Services;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Financeiro
{
    [Authorize]
    [ApiController]
    [Route("api/financeiro/[controller]")]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _services;

        public FaturaController(IFaturaService services)
        {
            _services = services;
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] FaturaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("visao-geral")]
        public async Task<ActionResult> ObterVisaoGeral([FromQuery] FaturaFilterInput filter)
        {
            return await _services.ObterVisaoGeral(filter);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] FaturaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] FaturaPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            return await _services.Excluir(id);
        }
    }
}
