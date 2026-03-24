
namespace Estac.Domain.Output.Auth
{
    public class UsuarioRoleOuput
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public int? EstacionamentoId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public List<PermissionOutput> Permissions { get; set; } = new();
    }
}