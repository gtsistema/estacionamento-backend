using Estac.Domain.Auth;
using Estac.Domain.Input.Pessoa;
using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class RegisterInput
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public int EstacionamentoId { get; set; }
        public PessoaUsuarioImput Pessoa { get; set; }
        public ApplicationRole Perfil { get; set; }
    }
}