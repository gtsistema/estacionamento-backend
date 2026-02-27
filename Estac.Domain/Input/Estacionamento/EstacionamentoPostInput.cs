using Estac.Domain.Input.Base;
using Estac.Domain.Input.Pessoa;

namespace Estac.Domain.Input.Estacionamento
{
    public class EstacionamentoPostInput : BaseIntInput
    {
        public int PessoaId { get; set; }
        public int? CapacidadeVeiculo { get; set; }
        public bool Tamanho { get; set; }
        public bool? PossuiSeguranca { get; set; }
        public bool? PossuiBanheiro { get; set; }
        public PessoaInput Pessoa { get; set; }
    }
}