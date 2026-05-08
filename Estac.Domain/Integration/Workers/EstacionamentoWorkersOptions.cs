namespace Estac.Domain.Integration.Workers
{
    /// <summary>Configuração da API estacionamento-workers (Redis + SignalR).</summary>
    public sealed class EstacionamentoWorkersOptions
    {
        public const string SectionName = "EstacionamentoWorkers";

        /// <summary>Quando false, não envia notificações (útil em testes ou ambiente isolado).</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>URL base do workers, ex.: http://localhost:5030</summary>
        public string BaseUrl { get; set; } = string.Empty;

        public int TimeoutSeconds { get; set; } = 15;
    }
}
