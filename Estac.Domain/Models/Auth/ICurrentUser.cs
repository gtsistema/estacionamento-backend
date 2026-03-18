using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Estac.Domain.Models.Auth
{
    public interface ICurrentUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim> Claims { get; }
        bool IsInRole(string role);
        Guid EmpresaId { get; }
    }
}