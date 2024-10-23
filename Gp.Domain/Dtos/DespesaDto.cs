using Gp.Domain.Dtos.Base;
using Gp.Domain.Extensions;
using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Dtos
{
    public class DespesaDto : BaseDto
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao => TipoDespesa.GetDescription();
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante => ValorPrevisto - ValorTotal;
        public decimal PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
    }
}
