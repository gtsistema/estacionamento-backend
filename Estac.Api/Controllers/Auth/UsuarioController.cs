using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Auth
{
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterInput login)
        {
            return await _services.RegisterAsync(login);
        }
    }
}
