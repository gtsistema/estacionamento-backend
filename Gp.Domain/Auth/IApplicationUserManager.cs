using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Gp.Domain.Auth
{
    public interface IApplicationUserManager
    {
        Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(Guid id);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<string> GerarTokenDeConfirmacaoDeEmailAsync(ApplicationUser user);
        Task<IEnumerable<string>> ConfirmEmailAsync(Guid userId, string token);
        Task<string> GerarTokenDeRecuperacaoDeSenhaAsync(ApplicationUser email);
        Task<IEnumerable<string>> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<IEnumerable<string>> AddToRoleAsync(ApplicationUser user, string role);
        Task AddToRoleAsync(Guid userId, string role);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword);
        public PasswordVerificationResult CheckPassword(ApplicationUser user, string password);
        Task<IEnumerable<string>> AddLoginAsync(ApplicationUser user, string loginProvider, string providerKey,
            string nome);
    }
}