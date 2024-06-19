using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gp.Domain.Models.ModelEnum
{
    public enum TipoReceita
    {
        [Display(Name = "Salário Jean")]
        SalarioJean = 1,

        [Display(Name = "Bpc Lorenzo")]
        Bpc = 2,

        [Display(Name = "Salario Carol")]
        SalarioCarol = 3,

        [Display(Name = "Revenda de Produtos")]
        RevendaProduto = 4,

        [Display(Name = "Serviços Extras")]
        Extra = 5,

        [Display(Name = "Flex Benner")]
        FlexBenner = 6,
    }
}
