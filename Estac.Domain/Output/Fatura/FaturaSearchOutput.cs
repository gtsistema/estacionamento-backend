using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Fatura
{
    public class FaturaSearchOutput
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int TransportadoraId { get; set; }
        public string TransportadoraNome { get; set; }
        public int EstacionamentoId { get; set; }
        public string EstacionamentoNome { get; set; }
        public StatusFatura Status { get; set; }
        public ModalidadeRecebimento? ModalidadeRecebimento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal ValorEmAberto { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
