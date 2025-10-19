using System;

namespace Estac.Domain.Clock

{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
        DateTimeOffset UnixEpoch { get; }
        DateTimeOffset Brasilia { get; }
    }
}