using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Estac.Domain.Models.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _acessor;

        public CurrentUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor?.HttpContext?.User?.Identity?.Name;

        public string Email => IsAuthenticated ? _acessor.HttpContext.User.UserEmail() : string.Empty;

        public int Id => IsAuthenticated ? Convert.ToInt32(_acessor.HttpContext.User.UserId()) : 0;
        public bool IsAuthenticated => _acessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> Claims => _acessor.HttpContext.User.Claims;

        public bool IsInRole(string role) => _acessor.HttpContext.User.IsInRole(role);

        public int EmpresaId
        {
            get
            {
                var empresaId = _acessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

                if (int.TryParse(empresaId, out var id) && id > 0)
                    return id;

                return 0;
            }
        }

        public int RoleId
        {
            get
            {
                var empresaId = _acessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == "RoleId")?.Value;

                if (int.TryParse(empresaId, out var id) && id > 0)
                    return id;

                return 0;
            }
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string UserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}