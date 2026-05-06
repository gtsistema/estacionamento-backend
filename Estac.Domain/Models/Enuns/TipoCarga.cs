using System.ComponentModel;

namespace Estac.Domain.Models.Enuns
{
    public enum TipoCarga : byte
    {
        [Description("Seca")]
        Seca = 1,

        [Description("Refrigerada")]
        Refrigerada = 2,

        [Description("Perigosa")]
        Perigosa = 3,

        [Description("Granel")]
        Granel = 4,

        [Description("Líquida")]
        Liquida = 5
    }
}
