using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;

namespace Estac.Domain.Models.Auth
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}