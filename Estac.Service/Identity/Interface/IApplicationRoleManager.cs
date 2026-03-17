using Estac.Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace Estac.Service.Identity.Interface
{
    public interface IApplicationRoleManager
    {
        public Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateAsync(ApplicationRole role);

        Task<IEnumerable<ApplicationRole>> ListAsync();

        Task<ApplicationRole> FindByIdAsync(int id);

        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);
    }
}