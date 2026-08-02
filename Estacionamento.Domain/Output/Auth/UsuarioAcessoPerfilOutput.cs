using Estac.Domain.Input.Auth;

namespace Estac.Domain.Output.Auth
{
    public class UsuarioAcessoPerfilOutput
    {
        public UsuarioOutput Usuario { get; set; }
        public RoleOutput Role { get; set; }
        public List<MenuOutput> Menus { get; set; } = new();
        public TokenResponse Jwt { get; set; }
    }
}