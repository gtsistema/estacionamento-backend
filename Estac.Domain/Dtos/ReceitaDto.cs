using Estac.Domain.Extensions;
using Estac.Domain.Models.ModelEnum;
using Estac.Domain.Output.Base;

namespace Estac.Domain.Dtos
{
    public class ReceitaDto : BaseOutput
    {
        public TipoReceita TipoReceita { get; set; }
        public string Descricao => TipoReceita.GetDescription();
        public decimal ValorPrevisto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoRestante { get; set; }
        public decimal PorcentagemPaga { get; set; }
    }
}
