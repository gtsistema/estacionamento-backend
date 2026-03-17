
namespace Estac.Domain.Models.Auth
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
