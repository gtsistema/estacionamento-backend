using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Input.Auth
{
    public class RegisterInput
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }
    }
}
