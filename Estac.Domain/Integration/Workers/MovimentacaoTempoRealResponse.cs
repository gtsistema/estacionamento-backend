namespace Estac.Domain.Integration.Workers
{
    /// <summary>Resposta do POST movimentacaoTempoReal do estacionamento-workers.</summary>
    public sealed class MovimentacaoTempoRealResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Hub { get; set; } = string.Empty;
    }
}
