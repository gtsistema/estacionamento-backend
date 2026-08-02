using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class RedefinirSenhaInput
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token é obrigatório.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Nova senha é obrigatória.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória.")]
        public string ConfirmPassword { get; set; }
    }
}
