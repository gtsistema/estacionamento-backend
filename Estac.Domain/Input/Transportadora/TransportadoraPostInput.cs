using Estac.Domain.Input.Base;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Input.PessoaContato;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraPostInput
    {
        public PessoaInput Transportadora { get; set; } = new PessoaInput();
    }
}