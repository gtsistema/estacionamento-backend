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
        private readonly IClock _clock;

        private EstacionamentoConfiguracao _cache;
        private bool _carregado;

        public EstacionamentoContexto(
            ICurrentUser currentUser,
            IEstacionamentoConfiguracaoRepositories configuracaoRepositories,
            IClock clock)
        {
            _currentUser = currentUser;
            _configuracaoRepositories = configuracaoRepositories;
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

        public DateTime AgoraUtc() => _clock.UtcNow.UtcDateTime;

        public async Task<EstacionamentoConfiguracao> ObterConfiguracaoAsync()
        {
            if (_carregado)
                return _cache;

            _cache = await _configuracaoRepositories.ObterPorEstacionamentoIdAsync(EstacionamentoId);
            _carregado = true;
            return _cache;
        }

        public async Task<string> ObterTimeZoneIdAsync()
        {
            var config = await ObterConfiguracaoAsync();
            if (config != null && config.Ativo && !string.IsNullOrWhiteSpace(config.TimeZoneId))
                return config.TimeZoneId;

            return TimeZoneHelper.DefaultTimeZoneId;
        }

        public async Task<DateTime> ParaUtcAsync(DateTime dataLocal)
        {
            var tz = await ObterTimeZoneIdAsync();
            return TimeZoneHelper.ToUtc(dataLocal, tz);
        }

        public async Task<DateTime> ParaLocalAsync(DateTime dataUtc)
        {
            var tz = await ObterTimeZoneIdAsync();
            return TimeZoneHelper.FromUtc(dataUtc, tz);
        }
    }
}
