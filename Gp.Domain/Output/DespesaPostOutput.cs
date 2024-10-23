using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Output
{
    public class DespesaPostOutput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public decimal ValorTotal { get; set; }
        public int OrcamentoId { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal SaldoRestante => ValorPrevisto - ValorTotal;
        public decimal PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
    }
}
