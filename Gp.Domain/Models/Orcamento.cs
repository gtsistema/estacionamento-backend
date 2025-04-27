using Gp.Domain.Models.Enuns;
using Gp.Domain.Models.ModelEnum;

namespace Gp.Domain.Models
{
    public class Orcamento : Base
    {
        public MesDoAno? MesDoAno { get; set; }
        public int Ano { get; set; }
        public decimal ValorTotalDespesas { get; set; }
        public decimal ValorTotalReceitas { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<Receita> Receitas { get; set; }

        public decimal CalcularValorRestante()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }

        public decimal CalcularPorcentagemGastaDespesas()
        {
            return ValorTotalDespesas / (ValorTotalDespesas + ValorTotalReceitas) * 100;
        }

        public decimal CalcularPorcentagemGastaReceitas()
        {
            return ValorTotalReceitas / (ValorTotalDespesas + ValorTotalReceitas) * 100;
        }

        public decimal CalcularValorTotalFaltaGastar()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }

        public decimal CalcularValorFaltaParaReceitas()
        {
            return ValorTotalReceitas - ValorTotalDespesas;
        }
    }
}
