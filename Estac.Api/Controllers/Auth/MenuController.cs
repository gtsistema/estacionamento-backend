using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services;
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

        [HttpGet]
        public async Task<ActionResult> Buscar(MenuFilterInput filter)
        {
           return await _services.Buscar(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

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

        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] MenuUpdateInput input)
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
