using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Estac.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Estac.Service.Identity.Interface
{
    public interface IApplicationUserManager
    {
        Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(int id);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<string> GerarTokenDeConfirmacaoDeEmailAsync(ApplicationUser user);
        Task<IEnumerable<string>> ConfirmEmailAsync(int userId, string token);
        Task<string> GerarTokenDeRecuperacaoDeSenhaAsync(ApplicationUser email);
        Task<IEnumerable<string>> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<IEnumerable<string>> AddToRoleAsync(ApplicationUser user, string role);
        Task AddToRoleAsync(int userId, string role);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword);
        public PasswordVerificationResult CheckPassword(ApplicationUser user, string password);
        Task<IEnumerable<string>> AddLoginAsync(ApplicationUser user, string loginProvider, string providerKey,
            string nome);
    }
}