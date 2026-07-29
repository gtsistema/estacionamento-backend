using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class ConfiguracaoAgendamento
    {
        public Guid Id { get; set; }
        public int ConfiguracaoCobrancaId { get; set; }
        public TipoJob TipoJob { get; set; }
        public Periodicidade Periodicidade { get; set; }
        public int Intervalo { get; set; } = 1;
        public DayOfWeek? DiaSemana { get; set; }
        public int? DiaMes { get; set; }
        public TimeSpan HoraExecucao { get; set; }
        public DateTime? UltimaExecucao { get; set; }
        public DateTime? ProximaExecucao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public ConfiguracaoCobranca ConfiguracaoCobranca { get; set; }
    }
}
