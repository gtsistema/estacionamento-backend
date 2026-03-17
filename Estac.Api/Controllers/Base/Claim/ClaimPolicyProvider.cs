using Estac.Api.Controllers.Base.Claim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Estac.Api.Controllers.Base.Claim
{
    public class ClaimPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public ClaimPolicyProvider(
            IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("claim:"))
            {
                var claim = policyName.Substring("claim:".Length);

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new ClaimRequirement(claim))
                    .Build();

                return Task.FromResult<AuthorizationPolicy>(policy);
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
