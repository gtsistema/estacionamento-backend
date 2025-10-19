using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class Despesa : Base
    {
        public TipoDespesa TipoDespesa { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPago { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
        public long OrcamentoId { get; set; }
        public MesDoAno MesReferente { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<DespesaLancamento> DespesaLancamentos { get; set; }
        public virtual ICollection<DespesaPagamento> DespesaPagamentos { get; set; }
    }
}
