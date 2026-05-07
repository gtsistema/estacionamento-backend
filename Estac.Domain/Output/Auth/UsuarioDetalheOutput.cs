using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Auth;

namespace Estac.Domain.Output.Auth
{
    public class UsuarioDetalheOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int EstacionamentoId { get; set; }
        public PessoaUsuarioInput Pessoa { get; set; }
        public ApplicationRole Perfil { get; set; }
    }
}
