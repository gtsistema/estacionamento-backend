using Microsoft.AspNetCore.Authorization;

namespace Estac.Domain.Permission
{
    public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
    {
        public const string TokenUseClaimType = "token_use";
        public const string TokenUseApiInterna = "api_interna";

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // Token de sistema (Secret → obterToken): acesso total, sem claims de permissão de usuário.
            var isApiInterna = context.User.Claims.Any(c =>
                c.Type == TokenUseClaimType &&
                string.Equals(c.Value, TokenUseApiInterna, StringComparison.OrdinalIgnoreCase));

            if (isApiInterna)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

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
