using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Claim
{
    public class ClaimAuthorizationHandler
    : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ClaimRequirement requirement)
        {
            var hasClaim = context.User.Claims
                .Any(c => c.Type == "Claim" &&
                          c.Value == requirement.Claim);

            if (hasClaim)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
