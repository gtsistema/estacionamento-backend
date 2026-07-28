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
        public decimal? ValorEstadia { get; set; }
        public string EmailFinanceiro { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool? GerarFaturaAutomaticamente { get; set; }
    }
}
