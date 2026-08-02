using Microsoft.AspNetCore.Authorization;

namespace Estac.Domain.Permission
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string Permission)
        {
            Policy = $"permission:{Permission}";
        }
    }
}
