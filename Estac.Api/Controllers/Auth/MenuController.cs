using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Controllers.Base.Permission;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Auth
{
    [Authorize]
    [ApiController]
    [Route("api/auth/[controller]/[action]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _services;

        public MenuController(IMenuServices _services)
        {
            this._services = _services;
        }

        [PermissionAuthorize(PermissionAcess.Menu.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar()
        {
           return await _services.Buscar();
        }

        [PermissionAuthorize(PermissionAcess.Menu.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Menu.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] MenuCreateInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _services.Gravar(input);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [PermissionAuthorize(PermissionAcess.Menu.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] MenuUpdateInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Menu.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }

        [PermissionAuthorize(PermissionAcess.Menu.Alterar)]
        [HttpPut]
        public async Task<ActionResult> OrganizarMenus(MenuOrganizacaoInput input)
        {
            return await _services.OrganizarMenus(input);
        }

        [PermissionAuthorize(PermissionAcess.Menu.Excluir)]
        [HttpDelete("{idSubMenu}")]
        public async Task<ActionResult> DeleteSubMenu(int idSubMenu)
        {
            return await _services.ExcluirSubMenu(idSubMenu);
        }
        [PermissionAuthorize(PermissionAcess.Menu.Excluir)]
        [HttpDelete("{idPermissao}")]
        public async Task<ActionResult> DeletePermissao(int idPermissao)
        {
            return await _services.ExcluirPermissao(idPermissao);
        }
    }
}
