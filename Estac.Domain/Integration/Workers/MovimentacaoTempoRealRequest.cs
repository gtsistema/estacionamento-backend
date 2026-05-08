namespace Estac.Domain.Integration.Workers
{
    /// <summary>Payload alinhado ao <c>MovimentacaoTempoRealDto</c> do estacionamento-workers.</summary>
    public sealed class MovimentacaoTempoRealRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Placa { get; set; } = string.Empty;
        public string Motorista { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Transportadora { get; set; } = string.Empty;
        public string TipoCarga { get; set; } = string.Empty;
        public string StatusMovimentacao { get; set; } = string.Empty;
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public string TempoPermanencia { get; set; } = string.Empty;
        public string Patio { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}
