using System.Threading.Tasks;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public class ApplicationSignManager : IApplicationSignManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationSignManager(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password)
        {
           return await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: true);
        }

        public async Task<SignInResult> ExternalSignInAssync(string loginProvider, string providerKey)
        {
            return await _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, false);
        }
    }
}