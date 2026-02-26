using Estac.Domain.Output.Base;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Output.Estacionamento
{
    public class EstacionamentoOutput : BaseOutput
    {
        public int PessoaId { get; set; }
        public PessoaOutput Pessoa { get; set; }
    }
}