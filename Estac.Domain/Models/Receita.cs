using Estac.Domain.Models.Enuns;
using Estac.Domain.Models.ModelEnum;

namespace Estac.Domain.Models
{
    public class Receita : Base
    {
        public TipoReceita TipoReceita { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemRecebido { get; set; }
        public long OrcamentoId { get; set; }
        public MesDoAno MesReferente { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<ReceitaLancamento> ReceitaLancamentos { get; set; }
    }
}
