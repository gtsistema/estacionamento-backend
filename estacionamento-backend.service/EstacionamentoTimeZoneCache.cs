using Estac.Domain.Interface.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Estac.Service
{
    public class EstacionamentoTimeZoneCache : IEstacionamentoTimeZoneCache
    {
        private const string KeyPrefix = "estac:tz:";
        private static readonly TimeSpan SlidingExpiration = TimeSpan.FromHours(12);

        private readonly IMemoryCache _cache;

        public EstacionamentoTimeZoneCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set(int estacionamentoId, string timeZoneId)
        {
            if (estacionamentoId <= 0)
                return;

            if (string.IsNullOrWhiteSpace(timeZoneId))
            {
                Remove(estacionamentoId);
                return;
            }

            _cache.Set(
                BuildKey(estacionamentoId),
                timeZoneId.Trim(),
                new MemoryCacheEntryOptions { SlidingExpiration = SlidingExpiration });
        }

        public bool TryGet(int estacionamentoId, out string timeZoneId)
        {
            timeZoneId = null;
            if (estacionamentoId <= 0)
                return false;

            if (_cache.TryGetValue(BuildKey(estacionamentoId), out string cached) && !string.IsNullOrWhiteSpace(cached))
            {
                timeZoneId = cached;
                return true;
            }

            return false;
        }

        public void Remove(int estacionamentoId)
        {
            if (estacionamentoId > 0)
                _cache.Remove(BuildKey(estacionamentoId));
        }

        private static string BuildKey(int estacionamentoId) => KeyPrefix + estacionamentoId;
    }
}
