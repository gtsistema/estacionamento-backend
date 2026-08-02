using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Estac.Domain.Models.Auth
{
    public interface ICurrentUser
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim> Claims { get; }
        bool IsInRole(string role);
        int EmpresaId { get; }
        /// <summary>Fuso IANA do estacionamento (claim TimeZoneId no JWT), se houver.</summary>
        string TimeZoneId { get; }
    }
}