using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Fatura
{
    public class FaturaFilterInput : FilterInput
    {
        public int? TransportadoraId { get; set; }
        public int? EstacionamentoId { get; set; }
        public StatusFatura? Status { get; set; }
        public ModalidadeRecebimento? ModalidadeRecebimento { get; set; }
        public string Numero { get; set; }
    }
}
