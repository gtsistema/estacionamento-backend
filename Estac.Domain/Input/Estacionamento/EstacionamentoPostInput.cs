using Estac.Domain.Input.Base;
using Estac.Domain.Input.Pessoa;

namespace Estac.Domain.Input.Estacionamento
{
    public class EstacionamentoPostInput : BaseIntInput
    {
        public int PessoaId { get; set; }
        public PessoaInput Pessoa { get; set; }
    }
}