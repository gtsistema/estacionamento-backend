using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaUsuarioImput
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Documento { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}
