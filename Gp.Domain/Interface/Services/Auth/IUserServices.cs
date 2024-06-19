using Gp.Domain.Input.Auth;
using Gp.Domain.Output;

namespace Gp.Domain.Interface.Services.Auth
{
    public interface IUserServices
    {
        Task<ServicesResult> LoginAsync(LoginInput dto);
        Task<ServicesResult> RegisterAsync(RegisterInput dto);
    }
}
