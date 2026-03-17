
using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Claim
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string Claim { get; }

        public ClaimRequirement(string claim)
        {
            Claim = claim;
        }
    }
}