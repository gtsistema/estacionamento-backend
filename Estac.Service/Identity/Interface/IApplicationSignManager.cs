using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Estac.Service.Identity.Interface
{
    public interface IApplicationSignManager
    {
        Task<SignInResult> PasswordSignInAsync(string email, string password);
        Task<SignInResult> ExternalSignInAssync(string loginProvider, string providerKey);
    }
}