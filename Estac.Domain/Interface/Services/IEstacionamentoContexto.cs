using Estac.Domain.Models;

namespace Estac.Domain.Interface.Services
{
    /// <summary>
    /// Contexto do estacionamento do usuário logado.
    /// TimeZone: cache memória → claim JWT → banco → fallback DateTime.Now.
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
        /// Agora no fuso do estacionamento (ex.: Cuiabá). Sem config → DateTime.Now.
        /// Valor usado na persistência das movimentações.
        /// </summary>
        DateTime AgoraUtc();

        /// <summary>Converte horário informado pelo client (local do pátio) para persistência.</summary>
        Task<DateTime> ParaUtcAsync(DateTime dataLocal);

        /// <summary>Converte valor persistido para exibição no fuso do estacionamento.</summary>
        Task<DateTime> ParaLocalAsync(DateTime dataPersistida);
    }
}
