using System.Security.Claims;
using Estac.Domain.Extensions.Notifier;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly INotifier _notifier;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager,
            IApplicationRoleManager roleManager, INotifier notifier)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notifier = notifier;
        }

        public async Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return new ApplicationIdentityResult(result.Errors.Select(e => e.Description));
        }

        public PasswordVerificationResult CheckPassword(ApplicationUser user, string password)
        {
            return _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }

        public async Task AddToRoleAsync(Guid userId, string role)
        {
            var user = await FindByIdAsync(userId);
            if (user is null)
                _notifier.Handle(new Notification("id", "Usuário não existe"));
            
            if (_notifier.HasNotification)
                return;

            var result = await AddToRoleAsync(user, role);
            if (result is not null)
            {
                foreach (var error in result)
                    _notifier.Handle(new Notification("", error));
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        {
            var result = await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }

            return await _userManager.AddPasswordAsync(user, newPassword);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<string> GerarTokenDeConfirmacaoDeEmailAsync(ApplicationUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IEnumerable<string>> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Errors.Select(t => t.Description);
        }

        public async Task<IEnumerable<string>> ResetPasswordAsync(ApplicationUser user, string token,
            string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Errors.Select(e => e.Description);
        }

        public async Task<string> GerarTokenDeRecuperacaoDeSenhaAsync(ApplicationUser user)
        {
            if (user is { })
                return await _userManager.GeneratePasswordResetTokenAsync(user);

            return null;
        }

        public async Task<IEnumerable<string>> AddLoginAsync(ApplicationUser user, string loginProvider,
            string providerKey, string nome)
        {
            var result = await _userManager.AddLoginAsync(user, new UserLoginInfo(loginProvider, providerKey, nome));
            return result.Errors.Select(e => e.Description);
        }

        public async Task<IEnumerable<string>> AddToRoleAsync(ApplicationUser user, string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var roleNew = new ApplicationRole { Name = role };

                var roleResult = await _roleManager.CreateAsync(roleNew);

                if (roleResult.Succeeded)
                    return null;
            }

            if (await _userManager.IsInRoleAsync(user, role))
                return null;

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Errors.Select(e => e.Description);
        }
    }
}