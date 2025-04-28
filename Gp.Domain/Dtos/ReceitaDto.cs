using Gp.Domain.Dtos.Base;
using Gp.Domain.Extensions;
using Gp.Domain.Models.ModelEnum;

namespace Gp.Domain.Dtos
{
    public class ReceitaDto : BaseDto
    {
        public TipoReceita TipoReceita { get; set; }
        public string Descricao => TipoReceita.GetDescription();
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
    }
}
