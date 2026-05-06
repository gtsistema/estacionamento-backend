using Estac.Domain.Output.Base;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Output.Transportadora
{
    public class TransportadoraOutput : BaseOutput
    {
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }

        public PessoaTransportadoraOutput Pessoa { get; set; }
    }
}