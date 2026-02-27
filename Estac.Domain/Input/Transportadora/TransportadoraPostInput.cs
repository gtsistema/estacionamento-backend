using Estac.Domain.Input.Base;
using Estac.Domain.Input.Pessoa;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraPostInput : BaseIntInput
    {
        public string CNH { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public int PessoaId { get; set; }
        public PessoaInput Pessoa { get; set; }
    }
}