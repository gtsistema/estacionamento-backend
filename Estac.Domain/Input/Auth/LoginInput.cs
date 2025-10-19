using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }
    }
}
