using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.ConfiguracaoCobranca
{
    public class ConfiguracaoCobrancaSearchOutput
    {
        public int Id { get; set; }
        public int TransportadoraId { get; set; }
        public string TransportadoraNome { get; set; }
        public int EstacionamentoId { get; set; }
        public string EstacionamentoNome { get; set; }
        public StatusConfiguracaoCobranca Status { get; set; }
        public ModalidadeCobranca ModalidadeCobranca { get; set; }
        public byte? DiaFechamento { get; set; }
        public RegraFechamento RegraFechamento { get; set; }
        public int PrazoVencimentoDias { get; set; }
        public decimal? ValorEstacionamento { get; set; }
        public string EmailFinanceiro { get; set; }
        public bool EnvioAutomaticoEmail { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool? GerarFaturaAutomaticamente { get; set; }
    }
}
