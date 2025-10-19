using System.ComponentModel;

namespace Estac.Domain.Models.ModelEnum
{
    public enum TipoReceita
    {
        [Description("Salário Jean")]
        SalarioJean = 1,

        [Description("Bpc Lorenzo")]
        Bpc = 2,

        [Description("Salario Carol")]
        SalarioCarol = 3,

        [Description("Revenda de Produtos")]
        RevendaProduto = 4,

        [Description("Serviços Extras")]
        Extra = 5,

        [Description("Flex Benner")]
        FlexBenner = 6,
    }
}
