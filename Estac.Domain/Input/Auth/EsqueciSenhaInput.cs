using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class EsqueciSenhaInput
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
    }
}
