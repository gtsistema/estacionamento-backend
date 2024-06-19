using Gp.Domain.Extensions;
using Gp.Domain.Input;
using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Dtos
{
    public class DespesaGetDto : BaseInput
    {
        public TipoDespesa TipoDespesa { get; set; }
        public string Descricao => TipoDespesa.GetDescription();
        public float ValorPrevisto { get; set; }
        public float ValorTotal { get; set; }
        public float SaldoRestante => ValorPrevisto - ValorTotal;
        public float PorcentagemGasta => ValorTotal / ValorPrevisto * 100;
    }
}
