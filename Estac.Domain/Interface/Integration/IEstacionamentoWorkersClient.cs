using Estac.Domain.Integration.Workers;

namespace Estac.Domain.Interface.Integration
{
    public interface IEstacionamentoWorkersClient
    {
        /// <summary>POST movimentacaoTempoReal — workers persiste, publica Redis e notifica SignalR.</summary>
        Task RegistrarMovimentacaoTempoRealAsync(MovimentacaoTempoRealRequest request, CancellationToken cancellationToken = default);
    }
}
