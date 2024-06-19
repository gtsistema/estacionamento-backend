using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Gp.Domain.Auth
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public ApplicationRoleManager(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<bool> RoleExistsAsync(string roleName) =>
            _roleManager.RoleExistsAsync(roleName);

        public async Task<IEnumerable<string>> CreateAsync(IdentityRole<Guid> role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result.Errors.Select(t => t.Description);
        }
    }
}