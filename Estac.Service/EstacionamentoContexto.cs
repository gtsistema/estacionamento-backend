using Estac.Domain.Clock;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Shared;

namespace Estac.Service
{
    public class EstacionamentoContexto : IEstacionamentoContexto
    {
        private readonly ICurrentUser _currentUser;
        private readonly IEstacionamentoConfiguracaoRepositories _configuracaoRepositories;
        private readonly IEstacionamentoTimeZoneCache _timeZoneCache;
        private readonly IClock _clock;

        private EstacionamentoConfiguracao _cacheConfig;
        private bool _configCarregada;
        private string _timeZoneResolvido;
        private bool _timeZoneResolvidoDefinido;

        public EstacionamentoContexto(
            ICurrentUser currentUser,
            IEstacionamentoConfiguracaoRepositories configuracaoRepositories,
            IEstacionamentoTimeZoneCache timeZoneCache,
            IClock clock)
        {
            _currentUser = currentUser;
            _configuracaoRepositories = configuracaoRepositories;
            _timeZoneCache = timeZoneCache;
            _clock = clock;
        }

        public int EstacionamentoId
        {
            get
            {
                if (_currentUser.EmpresaId <= 0)
                    throw new InvalidOperationException("Usuário logado sem estacionamento vinculado.");
                return _currentUser.EmpresaId;
            }
        }

        public string TimeZoneId => ResolverTimeZoneIdSync();

        public bool PossuiConfiguracaoFuso => !string.IsNullOrWhiteSpace(TimeZoneId);

        public DateTime AgoraUtc()
        {
            // Com fuso configurado: persistimos em UTC de verdade.
            // Sem configuração: DateTime.Now para não quebrar o fluxo legado.
            if (PossuiConfiguracaoFuso)
                return _clock.UtcNow.UtcDateTime;

            return DateTime.Now;
        }

        public async Task<EstacionamentoConfiguracao> ObterConfiguracaoAsync()
        {
            if (_configCarregada)
                return _cacheConfig;

            if (_currentUser.EmpresaId <= 0)
            {
                _configCarregada = true;
                return null;
            }

            _cacheConfig = await _configuracaoRepositories.ObterPorEstacionamentoIdAsync(EstacionamentoId);
            _configCarregada = true;

            if (_cacheConfig != null && _cacheConfig.Ativo && !string.IsNullOrWhiteSpace(_cacheConfig.TimeZoneId))
                _timeZoneCache.Set(EstacionamentoId, _cacheConfig.TimeZoneId);

            return _cacheConfig;
        }

        public async Task<string> ObterTimeZoneIdAsync()
        {
            var sync = ResolverTimeZoneIdSync();
            if (!string.IsNullOrWhiteSpace(sync))
                return sync;

            var config = await ObterConfiguracaoAsync();
            if (config != null && config.Ativo && !string.IsNullOrWhiteSpace(config.TimeZoneId))
            {
                _timeZoneResolvido = config.TimeZoneId.Trim();
                _timeZoneResolvidoDefinido = true;
                return _timeZoneResolvido;
            }

            _timeZoneResolvido = null;
            _timeZoneResolvidoDefinido = true;
            return null;
        }

        public async Task<DateTime> ParaUtcAsync(DateTime dataLocal)
        {
            var tz = await ObterTimeZoneIdAsync();
            if (string.IsNullOrWhiteSpace(tz))
                return dataLocal;

            return TimeZoneHelper.ToUtc(dataLocal, tz);
        }

        public async Task<DateTime> ParaLocalAsync(DateTime dataUtc)
        {
            var tz = await ObterTimeZoneIdAsync();
            if (string.IsNullOrWhiteSpace(tz))
                return dataUtc;

            return TimeZoneHelper.FromUtc(dataUtc, tz);
        }

        /// <summary>
        /// Ordem: cache memória (atualizado no CRUD) → claim JWT (login). Sem banco.
        /// </summary>
        private string ResolverTimeZoneIdSync()
        {
            if (_timeZoneResolvidoDefinido)
                return _timeZoneResolvido;

            if (_currentUser.EmpresaId > 0 && _timeZoneCache.TryGet(_currentUser.EmpresaId, out var fromCache))
            {
                _timeZoneResolvido = fromCache;
                _timeZoneResolvidoDefinido = true;
                return _timeZoneResolvido;
            }

            var fromClaim = _currentUser.TimeZoneId;
            if (!string.IsNullOrWhiteSpace(fromClaim))
            {
                _timeZoneResolvido = fromClaim.Trim();
                _timeZoneResolvidoDefinido = true;
                if (_currentUser.EmpresaId > 0)
                    _timeZoneCache.Set(_currentUser.EmpresaId, _timeZoneResolvido);
                return _timeZoneResolvido;
            }

            return null;
        }
    }
}
