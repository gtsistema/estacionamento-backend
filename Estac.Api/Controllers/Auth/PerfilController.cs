using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Controllers.Base.Permission;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Auth
{
    [Authorize]
    [ApiController]
    [Route("api/auth/[controller]/[action]")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilServices _services;
        private readonly ILogger<PerfilController> _logger;

        public PerfilController(IPerfilServices _services, ILogger<PerfilController> logger)
        {
            this._services = _services;
            this._logger = logger;
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar()
        {
           return await _services.Buscar();
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] PerfilCreateInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _services.Gravar(input);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao executar PerfilController.Gravar");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] ApplicationRole input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Excluir)]

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Delete(id);
        }

        [PermissionAuthorize(PermissionAcess.Perfil.Visualizar)]
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult> Buscar(int usuarioId)
        {
            return await _services.BuscarPerfilPermissaoUsuario(usuarioId);
        }


        [PermissionAuthorize(PermissionAcess.Perfil.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Ordem([FromBody] ApplicationRole input)
        {
            return await _services.Alterar(input);
        }
    }
}
