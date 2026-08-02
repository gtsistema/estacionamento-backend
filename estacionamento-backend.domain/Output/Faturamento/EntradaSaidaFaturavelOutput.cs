namespace Estac.Domain.Output.Faturamento
{
    /// <summary>Fase 2 do job: movimento encerrado e ainda não vinculado a uma fatura.</summary>
    public class EntradaSaidaFaturavelOutput
    {
        public int Id { get; set; }
        public int EstacionamentoId { get; set; }
        public int? TransportadoraId { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public int MotoristaId { get; set; }
        public string MotoristaNome { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public int TempoPermanenciaMinutos { get; set; }
        public int TempoTotalSuspensaoMinutos { get; set; }
        public bool Faturado { get; set; }
        public DateTime? DataFaturado { get; set; }
    }
}
