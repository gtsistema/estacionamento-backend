namespace Estac.Domain.Models.Auth
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int SubModuleId { get; set; }
        public ApplicationRole Role { get; set; }
        public Permission Permission { get; set; }
        public SubModule SubModule { get; set; }
    }
}