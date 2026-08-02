using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Input.Auth
{
    public class ConfirmarEmailInput
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
