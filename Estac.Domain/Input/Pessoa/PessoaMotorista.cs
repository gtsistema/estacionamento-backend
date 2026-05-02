using Estac.Domain.Input.Base;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaMotorista
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