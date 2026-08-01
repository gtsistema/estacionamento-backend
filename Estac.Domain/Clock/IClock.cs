using System;

namespace Estac.Domain.Clock
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
        DateTimeOffset UnixEpoch { get; }
        DateTimeOffset Brasilia { get; }
        /// <summary>Horário de Cuiabá / MT (UTC-4).</summary>
        DateTimeOffset Cuiaba { get; }
    }
}
