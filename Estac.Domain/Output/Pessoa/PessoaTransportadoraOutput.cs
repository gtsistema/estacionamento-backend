using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;
namespace Estac.Domain.Output.Pessoa
{
    public class PessoaTransportadoraOutput : BaseOutput
    {
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public List<PessoaEnderecoOutput> Enderecos { get; set; } = new List<PessoaEnderecoOutput>();
        public List<PessoaContatoOutput> Contatos { get; set; } = new List<PessoaContatoOutput>();
    }
}
}
