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

        public Guid Id => IsAuthenticated ? Guid.Parse(_acessor.HttpContext.User.UserId()) : Guid.Empty;
        public bool IsAuthenticated => _acessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> Claims => _acessor.HttpContext.User.Claims;

        public bool IsInRole(string role) => _acessor.HttpContext.User.IsInRole(role);

        public Guid EmpresaId
        {
            get
            {
                var empresaId = _acessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "empresaId")?.Value;
                if (Guid.TryParse(empresaId, out var guidOutput) && guidOutput != Guid.Empty)
                    return guidOutput;

                return Guid.Empty;
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