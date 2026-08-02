using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Services;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoConfiguracaoController : ControllerBase
    {
        private readonly IEstacionamentoConfiguracaoService _services;

        public EstacionamentoConfiguracaoController(IEstacionamentoConfiguracaoService services)
        {
            _services = services;
        }

        /// <summary>
        /// Dropdown: lista timeZoneId + nome + utcOffset dos fusos do Brasil.
        /// Front usa timeZoneId como value e Nome como label.
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet("padroes")]
        public async Task<ActionResult> ListarPadroesBrasil()
        {
            return await _services.ListarPadroesBrasil();
        }

        /// <summary>Configuração do estacionamento do usuário logado (claim EmpresaId).</summary>
        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> ObterDoUsuario()
        {
            return await _services.ObterDoUsuario();
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet("estacionamento/{estacionamentoId:int}")]
        public async Task<ActionResult> ObterPorEstacionamentoId(int estacionamentoId)
        {
            return await _services.ObterPorEstacionamentoId(estacionamentoId);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] EstacionamentoConfiguracaoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] EstacionamentoConfiguracaoPutInput input)
        {
            return await _services.Alterar(input);
        }
    }
}
