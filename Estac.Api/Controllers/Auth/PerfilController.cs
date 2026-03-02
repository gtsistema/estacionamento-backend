using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Output.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Estac.Api.Controllers.Auth
{
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

        [HttpGet]
        public async Task<ActionResult> Buscar()
        {
           return await _services.Buscar();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return await _services.ObterPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] ApplicationRole input)
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

        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] ApplicationRole input)
        {
            return await _services.Alterar(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _services.Delete(id);
        }
    }
}
