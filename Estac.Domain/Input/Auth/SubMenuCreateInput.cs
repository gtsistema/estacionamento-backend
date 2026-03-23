
namespace Estac.Domain.Input.Auth
{
    public class SubMenuCreateInput
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public List<PermissionInput> Permissions { get; set; }
    }
}