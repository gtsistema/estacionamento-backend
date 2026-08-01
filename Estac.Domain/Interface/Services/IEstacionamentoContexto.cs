using Estac.Domain.Models;

namespace Estac.Domain.Interface.Services
{
    /// <summary>
    /// Contexto do estacionamento do usuário logado.
    /// TimeZone vem de: claim JWT (login) → cache memória → banco (1x) → fallback DateTime.Now.
    /// </summary>
    public interface IEstacionamentoContexto
    {
        int EstacionamentoId { get; }

        /// <summary>TimeZone IANA configurado, ou null se não houver configuração.</summary>
        string TimeZoneId { get; }

        /// <summary>True quando existe fuso configurado (claim/cache/banco).</summary>
        bool PossuiConfiguracaoFuso { get; }

        Task<EstacionamentoConfiguracao> ObterConfiguracaoAsync();
        Task<string> ObterTimeZoneIdAsync();

        /// <summary>
        /// Agora para persistência: UTC se houver fuso configurado; senão DateTime.Now (nunca nulo).
        /// </summary>
        DateTime AgoraUtc();

        Task<DateTime> ParaUtcAsync(DateTime dataLocal);
        Task<DateTime> ParaLocalAsync(DateTime dataUtc);
    }
}
