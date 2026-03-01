using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public interface IApplicationRoleManager
    {
        public Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateAsync(ApplicationRole role);

        Task<IEnumerable<ApplicationRole>> ListAsync();

        Task<ApplicationRole> FindByIdAsync(Guid id);

        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);
    }
}