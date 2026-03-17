using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Claim
{
    public class ClaimAuthorizeAttribute : AuthorizeAttribute
    {
        public ClaimAuthorizeAttribute(string Claim)
        {
            Policy = $"claim:{Claim}";
        }
    }
}
