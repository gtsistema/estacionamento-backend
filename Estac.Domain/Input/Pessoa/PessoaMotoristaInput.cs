using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaMotoristaInput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<PessoaEnderecoInput> Enderecos { get; set; } = new List<PessoaEnderecoInput>();
        public IEnumerable<PessoaContatoInput> Contatos { get; set; } = new List<PessoaContatoInput>();
    }
}