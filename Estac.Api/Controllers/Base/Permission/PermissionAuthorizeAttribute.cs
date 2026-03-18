using Microsoft.AspNetCore.Authorization;

namespace Estac.Api.Controllers.Base.Permission
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string Permission)
        {
            Policy = $"permission:{Permission}";
        }
    }
}
