
namespace Estac.Domain.Output.Auth
{
    public class UsuarioAcessOutput
    {
        public List<MenuAcessOuput> Menus { get; set; } = new();
        public TokenResponse Jwt { get; set; }
    }

    public class UsuarioOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public int? EstacionamentoId { get; set; }
        public string Estacionamento { get; set; }
        public int? TransportadoraId { get; set; }
        public string Transportadora { get; set; }
        public string Role { get; set; }
    }

    public class MenuAcessOuput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
        public string Rota { get; set; }
        public List<SubMenuAcessOuput> SubMenus { get; set; } = new();
    }

    public class SubMenuAcessOuput
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Descricao { get; set; }
        public string Rota { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}
