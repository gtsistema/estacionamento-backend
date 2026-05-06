using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaEstacionamentoInput
    {
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<PessoaEnderecoInput> Enderecos { get; set; } = new List<PessoaEnderecoInput>();
        public IEnumerable<PessoaContatoInput> Contatos { get; set; } = new List<PessoaContatoInput>();
    }
}
