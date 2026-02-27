using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Documento { get; set; }
    }
}
