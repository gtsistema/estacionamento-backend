using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Output.Auth
{
    public class UsuarioAcessoPerfilOutput
    {
        public UsuarioOutput Usuario { get; set; }
        public RoleOutput Role { get; set; }
        public List<MenuOuput> Menus { get; set; } = new();
        public TokenResponse Jwt { get; set; }
    }
}