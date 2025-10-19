using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Estac.Domain.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid? EmpresaId { get; set; }
        public string FullName { get; set; }
        public bool TemporaryPassword { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public string ImagemUrl { get; set; }
    }
}