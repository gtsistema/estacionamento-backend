using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Faturamento
{
    /// <summary>Valores e regras da ConfiguracaoCobranca usados no cálculo da fatura.</summary>
    public class RegrasCobrancaOutput
    {
        public RegraFechamento RegraFechamento { get; set; }
        public byte? DiaFechamento { get; set; }
        public DateTime? DataCobranca { get; set; }
        public int PrazoVencimentoDias { get; set; }
        public decimal? ValorEstacionamento { get; set; }
        public bool CobrarLavagem { get; set; }
        public decimal? ValorLavagem { get; set; }
        public bool CobrarPernoite { get; set; }
        public decimal? ValorPernoite { get; set; }
        public bool CobrarServicosExtras { get; set; }
        public decimal? ValorServicosExtras { get; set; }
        public bool ConsiderarBeneficioAbastecimento { get; set; }
        public decimal? ValorBeneficioAbastecimento { get; set; }
        public bool AplicarMulta { get; set; }
        public decimal MultaPercentual { get; set; }
        public bool AplicarJuros { get; set; }
        public decimal JurosPercentual { get; set; }
        public bool AplicarDescontoFixo { get; set; }
        public decimal ValorDescontoFixo { get; set; }
        public bool AplicarAcrescimoFixo { get; set; }
        public decimal ValorAcrescimoFixo { get; set; }
        public bool AgruparPorPlaca { get; set; }
        public bool AgruparPorPeriodo { get; set; }
        public bool AgruparPorTransportadora { get; set; }
        public bool EnvioAutomaticoEmail { get; set; }
        public string EmailFinanceiro { get; set; }
    }
}
