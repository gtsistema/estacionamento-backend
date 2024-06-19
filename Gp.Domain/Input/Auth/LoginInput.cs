using System.ComponentModel.DataAnnotations;

namespace Gp.Domain.Input.Auth
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
    }
}
