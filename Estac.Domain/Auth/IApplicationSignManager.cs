using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public interface IApplicationSignManager
    {
        Task<SignInResult> PasswordSignInAsync(string email, string password);
        Task<SignInResult> ExternalSignInAssync(string loginProvider, string providerKey);
    }
}