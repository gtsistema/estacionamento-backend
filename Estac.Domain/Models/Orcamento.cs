using Estac.Domain.Models.Enuns;
using Estac.Domain.Models.ModelEnum;

namespace Estac.Domain.Models
{
    public class Orcamento : Base
    {
        public int Ano { get; set; }
        public decimal? ValorPrevistoDespesa { get; set; }
        public decimal? ValorPrevistoReceita { get; set; }
        public decimal ValorTotalPagoDespesas { get; set; }
        public decimal ValorTotalPagoReceitas { get; set; }
        public DateTime DataLimite { get; set; }
        public bool NaoPossuiDataLimite { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<Receita> Receitas { get; set; }
    }
}
