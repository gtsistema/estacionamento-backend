using Gp.Domain.Models.Enuns;
using Gp.Domain.Models.ModelEnum;

namespace Gp.Domain.Models
{
    public class Orcamento : Base
    {
        public string Descricao { get; set; }
        public MesDoAno? MesDoAno { get; set; }
        public int Ano { get; set; }
        public float ValorTotalDespesas { get; set; }
        public float ValorTotalReceitas { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<Receita> Receitas { get; set; }

        public float CalcularValorRestante()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }

        public float CalcularPorcentagemGastaDespesas()
        {
            return ValorTotalDespesas / (ValorTotalDespesas + ValorTotalReceitas) * 100;
        }

        public float CalcularPorcentagemGastaReceitas()
        {
            return ValorTotalReceitas / (ValorTotalDespesas + ValorTotalReceitas) * 100;
        }

        public float CalcularValorTotalFaltaGastar()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }

        public float CalcularValorFaltaParaReceitas()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }
    }
}
