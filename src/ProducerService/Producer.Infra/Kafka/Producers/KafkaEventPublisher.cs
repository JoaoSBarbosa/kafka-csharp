using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Producer.Application.Ports.Messaging;
using Shared.Contracts.Abstractions;
using Shared.Contracts.Topics;

namespace Producer.Infra.Kafka.Producers;

public class KafkaEventPublisher : IEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaEventPublisher> _logger;
    public KafkaTopics Topic => KafkaTopics.Registered;

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
        var json = JsonSerializer.Serialize(@event);

        await _producer.ProduceAsync(Topic.Name, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = json
        });

        _logger.LogInformation($"[KAFKA PUBLISHER] {Topic.Name}: {json}]");
    }
}