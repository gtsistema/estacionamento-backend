namespace Estac.Domain.Integration.Redis
{
    public class RedisEventPublisherOptions
    {
        public const string SectionName = "RedisPublisher";

        public string MovimentacaoChannel { get; set; } = "estacionamento.movimentacao";
        public int RetryCount { get; set; } = 3;
        public int RetryDelayMs { get; set; } = 200;
    }
}
