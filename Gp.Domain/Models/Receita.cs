using Gp.Domain.Models.ModelEnum;

namespace Gp.Domain.Models
{
    public class Receita : Base
    {
        public TipoReceita TIpoReceita { get; set; }
        public string Descricao { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante => ValorPrevisto - ValorTotal;
        public decimal PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
        public long OrcamentoId { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<ReceitaLancamento> ReceitaLancamentos { get; set; }
    }
}
