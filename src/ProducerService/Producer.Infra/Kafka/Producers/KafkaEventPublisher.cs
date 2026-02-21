using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Producer.Application.Ports.Messaging;

namespace Producer.Infra.Kafka.Producers;

public class KafkaEventPublisher : IEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventPublisher> _logger;

    public KafkaEventPublisher(string bootstrapServer, ILogger<KafkaEventPublisher> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServer
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
    {
        // USANDO O NOME DO EVENTO COMO TOPICO
        var topic = typeof(TEvent).Name;
        var json = JsonSerializer.Serialize(@event);

        await _producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = json
        });

        _logger.LogInformation($"[KAFKA PUBLISHER] {topic}: {json}]");
    }
}