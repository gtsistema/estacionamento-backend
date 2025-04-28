using Gp.Domain.Models.Enuns;
using Gp.Domain.Models.ModelEnum;

namespace Gp.Domain.Models
{
    public class Receita : Base
    {
        public TipoReceita TIpoReceita { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
        public long OrcamentoId { get; set; }
        public MesDoAno MesReferente { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<ReceitaLancamento> ReceitaLancamentos { get; set; }
    }
}
