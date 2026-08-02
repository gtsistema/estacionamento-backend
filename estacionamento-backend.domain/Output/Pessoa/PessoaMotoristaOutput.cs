using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;

namespace Estac.Domain.Output.Pessoa
{
    public class PessoaMotoristaOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public List<PessoaEnderecoInput> Enderecos { get; set; } = new();
        public List<PessoaContatoInput> Contatos { get; set; } = new();
    }
}
