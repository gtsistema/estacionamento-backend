
namespace Estac.Domain.Output.Auth
{
    public class UsuarioAcessoOutput
    {
        public UsuarioOutput Usuario { get; set; }
        public RoleOutput Role { get; set; }
        public List<MenuOuput> Menus { get; set; } = new();
        public TokenResponse Jwt { get; set; }
    }

    public class UsuarioOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public int? EstacionamentoId { get; set; }
    }

    public class RoleOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class MenuOuput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }

        public List<SubMenuOuput> SubMenus { get; set; } = new();
    }
    public class SubMenuOuput
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Descricao { get; set; }
        public string Rota { get; set; }

        public List<PermissionOutput> Permissions { get; set; } = new();
    }

    public class PermissionOutput
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
        public int SubMenuId {  get; set; }   
        public string Acao { get; set; }
    }
}
