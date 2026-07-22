using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoCobrancaFilterInput : FilterInput
    {
        public int? TransportadoraId { get; set; }
        public int? EstacionamentoId { get; set; }
        public StatusConfiguracaoCobranca? Status { get; set; }
    }
}
