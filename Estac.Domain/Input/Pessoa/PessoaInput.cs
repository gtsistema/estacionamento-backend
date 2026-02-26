using Estac.Domain.Input.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaInput : BaseIntInput
    {
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
