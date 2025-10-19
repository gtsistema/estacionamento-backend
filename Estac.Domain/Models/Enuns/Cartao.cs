using System.ComponentModel;

namespace Estac.Domain.Models.ModelEnum
{
    public enum Cartao
    {
        [Description("Nubank")]
        Nubank = 1,

        [Description("Itáu Uniclass")]
        ItauUniclass = 2,

        [Description("Itáu Click")]
        ItauClick = 3,

        [Description("Itáu Assai")]
        itauAssai = 4,

        [Description("Bradesco")]
        Bradesco = 5,

        [Description("Mercado Pago")]
        MercadoPago = 6,

        [Description("Nubank Carol")]
        NubankCarol = 7
    }
}
