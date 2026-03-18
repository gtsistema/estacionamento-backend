using Estac.Api.Controllers.Base.Claim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Estac.Api.Controllers.Base.Claim
{
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(
            IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("permission:"))
            {
                var permission = policyName.Substring("permission:".Length);

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(permission))
                    .Build();

                return Task.FromResult<AuthorizationPolicy>(policy);
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
