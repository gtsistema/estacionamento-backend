using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Models.Auth
{
    public class ApplicationRole : IdentityRole<int>
    {
        public int? EmpresaId { get; set; }
        public bool Padrao { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}