using Estac.Domain.Models.Auth;

namespace Estac.Domain.Output.Auth
{
    public class PermissionRoleOutput
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public List<MenuOutput> Menus { get; set; } = new();
    }
}
