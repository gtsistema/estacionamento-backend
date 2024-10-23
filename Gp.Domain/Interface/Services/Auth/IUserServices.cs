using Gp.Domain.Input.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services.Auth
{
    public interface IUserServices
    {
        Task<ActionResult> LoginAsync(LoginInput dto);
        Task<ActionResult> RegisterAsync(RegisterInput dto);
    }
}
