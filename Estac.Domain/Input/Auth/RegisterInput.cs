using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class RegisterInput
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public int? EstacionamentoId { get; set; }
        public int? TransportadoraId { get; set; }
        public PessoaUsuarioImput Pessoa { get; set; }
        public ApplicationRole Perfil { get; set; }
    }
}