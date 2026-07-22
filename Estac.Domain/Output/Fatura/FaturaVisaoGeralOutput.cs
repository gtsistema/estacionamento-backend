using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Fatura
{
    public class FaturaVisaoGeralOutput
    {
        public decimal TotalAReceber { get; set; }
        public decimal Recebido { get; set; }
        public decimal EmAberto { get; set; }
        public decimal Vencido { get; set; }
        public decimal AVencer { get; set; }
        public int FaturasEmitidas { get; set; }
        public int FaturasVencidas { get; set; }
        public List<FaturaStatusResumoOutput> FaturasPorStatus { get; set; } = new();
        public List<FaturaModalidadeResumoOutput> RecebimentosPorModalidade { get; set; } = new();
        public List<FaturaEvolucaoMensalOutput> EvolucaoFaturamento { get; set; } = new();
    }

    public class FaturaStatusResumoOutput
    {
        public StatusFatura Status { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class FaturaModalidadeResumoOutput
    {
        public ModalidadeRecebimento Modalidade { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class FaturaEvolucaoMensalOutput
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal Valor { get; set; }
    }
}
