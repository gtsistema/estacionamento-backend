using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;

namespace Estac.Domain.Output.Pessoa
{
    public class PessoaMotoristaOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<PessoaEnderecoInput> Enderecos { get; set; } = new List<PessoaEnderecoInput>();
        public IEnumerable<PessoaContatoInput> Contatos { get; set; } = new List<PessoaContatoInput>();
    }
}
