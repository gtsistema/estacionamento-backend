using System;

namespace Estac.Domain.Clock
{
    public class Clock : IClock
    {
        private static readonly TimeZoneInfo BrasiliaTz = ResolverFuso(
            windowsId: "E. South America Standard Time",
            ianaId: "America/Sao_Paulo");

        private static readonly TimeZoneInfo CuiabaTz = ResolverFuso(
            windowsId: "Central Brazilian Standard Time",
            ianaId: "America/Cuiaba");

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
        public DateTimeOffset Brasilia => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, BrasiliaTz);
        public DateTimeOffset Cuiaba => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, CuiabaTz);
        public DateTimeOffset UnixEpoch => DateTimeOffset.UnixEpoch;

        private static TimeZoneInfo ResolverFuso(string windowsId, string ianaId)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
            }
            catch (TimeZoneNotFoundException)
            {
                return TimeZoneInfo.FindSystemTimeZoneById(ianaId);
            }
            catch (InvalidTimeZoneException)
            {
                return TimeZoneInfo.FindSystemTimeZoneById(ianaId);
            }
        }
    }
}
