
using Microsoft.AspNetCore.Authorization;

namespace Estac.Domain.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string claim)
        {
            Permission = claim;
        }
    }
}