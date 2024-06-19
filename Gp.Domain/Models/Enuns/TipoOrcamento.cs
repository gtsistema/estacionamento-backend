using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gp.Domain.Models.Enuns
{
    public enum TipoOrcamento
    {
        [Display(Name = "Mensal")]
        Mensal = 1,

        [Display(Name = "Viagem")]
        Viagem = 2,

        [Display(Name = "Compra de carro")]
        Carro = 3,

        [Display(Name = "Compra de Imovel")]
        CompraImovel = 4,

        [Display(Name = "Curso, Faculdade, Inglês")]
        Curso = 5,

        [Display(Name = "Manutenção Veiculo")]
        ManutencaoCarro = 6,

        [Display(Name = "Investimento")]
        Investimento = 7,
    }
}
