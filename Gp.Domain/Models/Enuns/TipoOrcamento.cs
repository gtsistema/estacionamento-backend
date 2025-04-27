using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gp.Domain.Models.Enuns
{
    public enum TipoOrcamento
    {
        [Description("Mensal")]
        Mensal = 1,

        [Description("Viagem")]
        Viagem = 2,

        [Description("Compra de carro")]
        Carro = 3,

        [Description("Compra de Imovel")]
        CompraImovel = 4,

        [Description("Curso, Faculdade, Inglês")]
        Curso = 5,

        [Description("Manutenção Veiculo")]
        ManutencaoCarro = 6,

        [Description("Investimento")]
        Investimento = 7,
    }
}
