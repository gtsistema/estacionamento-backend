using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;

namespace Estac.Domain.Output.Pessoa
{
    public class PessoaOutput : BaseOutput
    {
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; } // CPF ou CNPJ
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
