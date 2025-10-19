using Estac.Domain.Dtos.Base;
using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Dtos
{
    public class DespesaDto : BaseDto
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao => TipoDespesa.GetDescription();
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
    }
}
