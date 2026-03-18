using Estac.Domain.Models.Auth;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Estac.Service.Identity
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleManager(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public Task<bool> RoleExistsAsync(string roleName) =>
            _roleManager.RoleExistsAsync(roleName);

        public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return await _roleManager.CreateAsync(role);
        }

        public async Task<IEnumerable<ApplicationRole>> ListAsync()
        {
            return await _roleManager.Roles.ToListAsync(); 
        }

        public async Task<ApplicationRole> FindByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }
    }
}