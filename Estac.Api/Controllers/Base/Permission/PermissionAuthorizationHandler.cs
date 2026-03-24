using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Claim
{
    public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var hasClaim = context.User.Claims
                .Any(c => c.Type == "permission" &&
                          c.Value == requirement.Permission);

            if (hasClaim)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
