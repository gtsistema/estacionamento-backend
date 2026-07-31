namespace Estac.Domain.Input.Faturamento
{
    public class RegistrarExecucaoAgendamentoInput
    {
        public Guid ConfiguracaoAgendamentoId { get; set; }
        public DateTime UltimaExecucao { get; set; }
        public DateTime ProximaExecucao { get; set; }

        /// <summary>Movimentos incluídos na fatura gerada nesta execução.</summary>
        public List<int> EntradaSaidaIds { get; set; } = new();
    }
}
