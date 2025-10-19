using System;

namespace Estac.Domain.Clock
{
    public class Clock : IClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
        public DateTimeOffset Brasilia => DateTimeOffset.UtcNow.AddHours(-3);
        public DateTimeOffset UnixEpoch => DateTimeOffset.UnixEpoch;
    }
}