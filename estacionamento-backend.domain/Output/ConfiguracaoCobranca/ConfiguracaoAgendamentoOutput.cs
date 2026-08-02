using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.ConfiguracaoCobranca
{
    public class ConfiguracaoAgendamentoOutput
    {
        public Guid Id { get; set; }
        public int ConfiguracaoCobrancaId { get; set; }
        public TipoJob TipoJob { get; set; }
        public ModalidadeCobranca ModalidadeCobranca { get; set; }
        public int Intervalo { get; set; }
        public DayOfWeek? DiaSemana { get; set; }
        public int? DiaMes { get; set; }
        public TimeSpan HoraExecucao { get; set; }
        public DateTime? UltimaExecucao { get; set; }
        public DateTime? ProximaExecucao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
