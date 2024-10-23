using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Models
{
    public class Despesa : Base
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante => ValorPrevisto - ValorTotal;
        public decimal PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
        public long OrcamentoId { get; set; }
        public int Quantidade { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<DespesaLancamento> DespesaLancamentos { get; set; }
    }
}
