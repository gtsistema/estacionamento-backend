
namespace Estac.Domain.Input.Auth
{
    public class ModuloInput
    {
        public int MenuId { get; set; }
        public bool Selecionado { get; set; }
        public List<SubModuloInput> SubMenus { get; set; } = new();
    }
    public class SubModuloInput
    {
        public int SubMenuId { get; set; }
        public bool Selecionado { get; set; }
        public List<PermissaoInput> Permissoes { get; set; } = new();
    }

    public class PermissaoInput
    {
        public int PermissaoId { get; set; }
        public bool Selecionado { get; set; }
    }
}
