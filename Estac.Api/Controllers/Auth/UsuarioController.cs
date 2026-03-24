using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Controllers.Base.Permission;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Auth
{
    [Authorize]
    [ApiController]
    [Route("api/auth/[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        public readonly IUserServices _services;

        public UsuarioController(IUserServices services)
        {
           _services = services;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginInput login)
        {
            return await _services.LoginAsync(login);
        }

        [PermissionAuthorize(PermissionAcess.Usuario.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterInput login)
        {
            return await _services.RegisterAsync(login);
        }
    }
}
