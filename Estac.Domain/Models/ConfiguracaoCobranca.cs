using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class ConfiguracaoCobranca : BaseInt
    {
        public int TransportadoraId { get; set; }
        public int EstacionamentoId { get; set; }
        public StatusConfiguracaoCobranca Status { get; set; }
        public ModalidadeCobranca ModalidadeCobranca { get; set; }
        public byte? DiaFechamento { get; set; }
        public RegraFechamento RegraFechamento { get; set; }
        public int PrazoVencimentoDias { get; set; }
        public string EmailFinanceiro { get; set; }
        public bool EnvioAutomaticoEmail { get; set; }
        public bool GerarFaturaAutomaticamente { get; set; }
        public bool PermitirPagamentoParcial { get; set; }
        public bool AplicarMulta { get; set; }
        public decimal MultaPercentual { get; set; }
        public bool AplicarJuros { get; set; }
        public decimal JurosPercentual { get; set; }
        public bool AplicarDescontoFixo { get; set; }
        public decimal ValorDescontoFixo { get; set; }
        public bool AplicarAcrescimoFixo { get; set; }
        public decimal ValorAcrescimoFixo { get; set; }
        public decimal? ValorEstacionamento { get; set; }
        /// <summary>Data de faturamento informada apenas quando a modalidade é Personalizado.</summary>
        public DateTime? DataCobranca { get; set; }
        public bool CobrarLavagem { get; set; }
        public decimal? ValorLavagem { get; set; }
        public bool CobrarPernoite { get; set; }
        public decimal? ValorPernoite { get; set; }
        public bool CobrarServicosExtras { get; set; }
        public decimal? ValorServicosExtras { get; set; }
        public bool ConsiderarBeneficioAbastecimento { get; set; }
        public decimal? ValorBeneficioAbastecimento { get; set; }
        public bool AgruparPorPlaca { get; set; }
        public bool AgruparPorPeriodo { get; set; }
        public bool AgruparPorTransportadora { get; set; }
        public Transportadora Transportadora { get; set; }
        public Estacionamento Estacionamento { get; set; }
        public ConfiguracaoAgendamento ConfiguracaoAgendamento { get; set; }
    }
}
