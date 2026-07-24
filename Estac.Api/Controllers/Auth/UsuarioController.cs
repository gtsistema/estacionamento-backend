using Estac.Domain.Permission;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Auth
{
    [Authorize]
    [ApiController]
    [Route("api/auth/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserServices _services;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUserServices services, ILogger<UsuarioController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginInput login)
        {
            return await _services.LoginAsync(login);
        }

        /// <summary>
        /// Obtém apenas o token JWT para APIs internas.
        /// Aceita UserName+Password <b>ou</b> Secret igual a BearerTokenSettings.Secret.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("obterToken")]
        public async Task<ActionResult> ObterToken([FromBody] ObterTokenInput input)
        {
            return await _services.ObterTokenAsync(input);
        }

        /// <summary>Chamado pelo front após o usuário abrir o link com userId e token (query do SPA).</summary>
        [AllowAnonymous]
        [HttpPost("confirmar-email")]
        public async Task<ActionResult> ConfirmarEmail([FromBody] ConfirmarEmailInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return await _services.ConfirmarEmailAsync(input);
        }

        /// <summary>Tela "Esqueci minha senha": envia e-mail com link para redefinir (resposta genérica por segurança).</summary>
        [AllowAnonymous]
        [HttpPost("esqueci-senha")]
        public async Task<ActionResult> EsqueciSenha([FromBody] EsqueciSenhaInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return await _services.EsqueciSenhaAsync(input);
        }

        /// <summary>Formulário de nova senha após o usuário abrir o link do e-mail (query: email, token).</summary>
        [AllowAnonymous]
        [HttpPost("redefinir-senha")]
        public async Task<ActionResult> RedefinirSenha([FromBody] RedefinirSenhaInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return await _services.RedefinirSenhaAsync(input);
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar()
        {
            return await _services.Buscar();
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Gravar)]
        [HttpPost("Register")]
        public async Task<ActionResult> Gravar([FromBody] RegisterInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _services.RegisterAsync(input);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao executar UsuarioController.Gravar");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Alterar)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Alterar(int id, [FromBody] RegisterInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return await _services.Alterar(id, input);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao executar UsuarioController.Alterar");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Delete(id);
        }
    }
}
