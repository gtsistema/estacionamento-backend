namespace Estac.Domain.Input.Faturamento
{
    /// <summary>
    /// Janela de competência com início inclusivo e fim exclusivo, para não depender
    /// da precisão de milissegundos do datetime.
    /// </summary>
    public class EntradaSaidaFaturavelFilterInput
    {
        public int EstacionamentoId { get; set; }
        public int TransportadoraId { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }

        /// <summary>Cursor do keyset: retorna somente registros com Id maior que este.</summary>
        public int? UltimoId { get; set; }

        public int Tamanho { get; set; } = 500;
    }
}
