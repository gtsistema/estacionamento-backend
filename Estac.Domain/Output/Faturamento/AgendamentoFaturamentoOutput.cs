using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Faturamento
{
    /// <summary>
    /// Fase 1 do job: um item por fatura a ser gerada. Traz tudo que o job precisa para
    /// delimitar o período e calcular os valores, sem novas idas ao banco.
    /// </summary>
    public class AgendamentoFaturamentoOutput
    {
        public Guid ConfiguracaoAgendamentoId { get; set; }
        public int ConfiguracaoCobrancaId { get; set; }
        public int TransportadoraId { get; set; }
        public string TransportadoraNome { get; set; }
        public int EstacionamentoId { get; set; }
        public string EstacionamentoNome { get; set; }
        public ModalidadeCobranca ModalidadeCobranca { get; set; }
        public int Intervalo { get; set; }
        public DayOfWeek? DiaSemana { get; set; }
        public int? DiaMes { get; set; }
        public TimeSpan HoraExecucao { get; set; }
        public DateTime? UltimaExecucao { get; set; }
        public DateTime? ProximaExecucao { get; set; }

        /// <summary>Fim do período da última fatura não cancelada; delimita o início do próximo período.</summary>
        public DateTime? UltimoPeriodoFaturado { get; set; }

        public RegrasCobrancaOutput Cobranca { get; set; }
    }
}
