using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public interface IApplicationRoleManager
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<IEnumerable<string>> CreateAsync(IdentityRole<Guid> role);
    }
}