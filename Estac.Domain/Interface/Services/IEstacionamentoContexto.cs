using Estac.Domain.Models;

namespace Estac.Domain.Interface.Services
{
    /// <summary>
    /// Contexto scoped do estacionamento do usuário logado.
    /// Carrega a configuração uma vez por request (melhor forma de recuperar o fuso).
    /// </summary>
    public interface IEstacionamentoContexto
    {
        int EstacionamentoId { get; }
        Task<EstacionamentoConfiguracao> ObterConfiguracaoAsync();
        Task<string> ObterTimeZoneIdAsync();
        /// <summary>Agora em UTC (para persistência).</summary>
        DateTime AgoraUtc();
        /// <summary>Converte valor local (ou Unspecified) do fuso do estacionamento para UTC.</summary>
        Task<DateTime> ParaUtcAsync(DateTime dataLocal);
        /// <summary>Converte UTC persistido para horário local do estacionamento (exibição).</summary>
        Task<DateTime> ParaLocalAsync(DateTime dataUtc);
    }
}
