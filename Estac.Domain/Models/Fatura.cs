using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class Fatura : BaseInt
    {
        public int TransportadoraId { get; set; }
        public int EstacionamentoId { get; set; }
        public int? ConfiguracaoCobrancaId { get; set; }
        public string Numero { get; set; }
        public StatusFatura Status { get; set; }
        public ModalidadeRecebimento? ModalidadeRecebimento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAcrescimo { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public string EmailEnvio { get; set; }
        public string Observacao { get; set; }
        public Transportadora Transportadora { get; set; }
        public Estacionamento Estacionamento { get; set; }
        public ConfiguracaoCobranca ConfiguracaoCobranca { get; set; }

        public decimal ValorEmAberto => Math.Max(0, ValorTotal - ValorRecebido);
    }
}
