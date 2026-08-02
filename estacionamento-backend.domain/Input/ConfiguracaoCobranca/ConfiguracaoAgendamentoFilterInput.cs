using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoAgendamentoFilterInput : FilterInput
    {
        public int? ConfiguracaoCobrancaId { get; set; }
        public TipoJob? TipoJob { get; set; }
        public ModalidadeCobranca? ModalidadeCobranca { get; set; }
        public bool? Ativo { get; set; }
    }
}
