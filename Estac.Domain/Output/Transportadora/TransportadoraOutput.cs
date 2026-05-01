using Estac.Domain.Output.Base;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Output.Transportadora
{
    public class TransportadoraOutput : BaseOutput
    {
        public PessoaOutput Pessoa { get; set; }
    }
}