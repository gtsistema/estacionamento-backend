using System;

namespace Gp.Domain.Clock

{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
        DateTimeOffset UnixEpoch { get; }
        DateTimeOffset Brasilia { get; }
    }
}