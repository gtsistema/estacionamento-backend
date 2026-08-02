namespace Estac.Domain.Input.Faturamento
{
    /// <summary>
    /// Filtro de movimentos faturáveis. Período é opcional (início inclusivo / fim exclusivo).
    /// Sem período, retorna todos Finalizado e não faturados da transportadora/estacionamento.
    /// </summary>
    public class EntradaSaidaFaturavelFilterInput
    {
        public int EstacionamentoId { get; set; }
        public int TransportadoraId { get; set; }

        /// <summary>Opcional. Quando informado com PeriodoFim, restringe por DataHoraSaida.</summary>
        public DateTime? PeriodoInicio { get; set; }

        /// <summary>Opcional. Quando informado com PeriodoInicio, restringe por DataHoraSaida.</summary>
        public DateTime? PeriodoFim { get; set; }

        /// <summary>Cursor do keyset: retorna somente registros com Id maior que este.</summary>
        public int? UltimoId { get; set; }

        public int Tamanho { get; set; } = 500;
    }
}
