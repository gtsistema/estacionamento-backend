
using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Claim
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