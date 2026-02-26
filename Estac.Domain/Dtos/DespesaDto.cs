using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;

namespace Estac.Domain.Dtos
{
    public class DespesaDto : BaseOutput
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao => TipoDespesa.GetDescription();
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
    }
}
