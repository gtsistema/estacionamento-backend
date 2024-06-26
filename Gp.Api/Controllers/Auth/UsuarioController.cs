using Gp.Domain.Input;
using Gp.Domain.Input.Auth;
using Gp.Domain.Interface.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/v1/auth/usuario")]
    public class UsuarioController : ControllerBase
    {
        public readonly IUserServices _services;

        public UsuarioController(IUserServices services)
        {
           _services = services;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginInput login)
        {
            var user = await _services.LoginAsync(login);

            return user.Error.Any() ? Unauthorized(user.Error) : Ok(user.Data);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterInput login)
        {
            var user = await _services.RegisterAsync(login);

            return user.Error.Any() ? BadRequest(user.Error) : Ok(user.Data);
        }
    }
}
