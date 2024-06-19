using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Models
{
    public class Despesa : Base
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao { get; set; }
        public float ValorPrevisto { get; set; }
        public float ValorTotal { get; set; }
        public float SaldoRestante => ValorPrevisto - ValorTotal;
        public float PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
        public int OrcamentoId { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<DespesaLancamento> DespesaLancamentos { get; set; }
    }
}
