using System;

namespace Estac.Domain.Shared
{
    /// <summary>
    /// Resolve fusos IANA (Linux/macOS/.NET moderno) e IDs Windows legados.
    /// </summary>
    public static class TimeZoneHelper
    {
        public const string DefaultTimeZoneId = "America/Cuiaba";

        public static TimeZoneInfo Resolve(string timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                timeZoneId = DefaultTimeZoneId;

            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId.Trim());
            }
            catch (TimeZoneNotFoundException)
            {
                return ResolveAlias(timeZoneId.Trim());
            }
            catch (InvalidTimeZoneException)
            {
                return ResolveAlias(timeZoneId.Trim());
            }
        }

        public static bool IsValid(string timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                return false;

            try
            {
                _ = Resolve(timeZoneId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToUtc(DateTime value, string timeZoneId)
        {
            var tz = Resolve(timeZoneId);
            if (value.Kind == DateTimeKind.Utc)
                return value;

            var unspecified = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(unspecified, tz);
        }

        public static DateTime FromUtc(DateTime utcValue, string timeZoneId)
        {
            var tz = Resolve(timeZoneId);
            var utc = utcValue.Kind == DateTimeKind.Utc
                ? utcValue
                : DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeFromUtc(utc, tz);
        }

        public static string FormatOffset(string timeZoneId)
        {
            var offset = Resolve(timeZoneId).GetUtcOffset(DateTime.UtcNow);
            var sign = offset < TimeSpan.Zero ? "-" : "+";
            var abs = offset.Duration();
            return $"{sign}{abs.Hours:00}:{abs.Minutes:00}";
        }

        private static TimeZoneInfo ResolveAlias(string timeZoneId)
        {
            // Mapeamento IANA <-> Windows para ambientes sem suporte nativo a IANA.
            var alias = timeZoneId switch
            {
                "America/Sao_Paulo" => "E. South America Standard Time",
                "America/Cuiaba" => "Central Brazilian Standard Time",
                "America/Manaus" => "SA Western Standard Time",
                "America/Belem" => "SA Eastern Standard Time",
                "America/Fortaleza" => "SA Eastern Standard Time",
                "America/Recife" => "SA Eastern Standard Time",
                "America/Bahia" => "Bahia Standard Time",
                "America/Rio_Branco" => "SA Pacific Standard Time",
                "America/Noronha" => "UTC-02",
                "E. South America Standard Time" => "America/Sao_Paulo",
                "Central Brazilian Standard Time" => "America/Cuiaba",
                _ => null
            };

            if (alias != null)
                return TimeZoneInfo.FindSystemTimeZoneById(alias);

            throw new TimeZoneNotFoundException($"Fuso horário não encontrado: {timeZoneId}");
        }
    }
}
