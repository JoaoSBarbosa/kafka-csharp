using Confluent.Kafka;
using Consumer.Application.Handlers;
using Consumer.Application.Ports.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Shared.Contracts.Events;

namespace Consumer.Infra.Kafka.Consumer;

public class KafkaConsumerWorker(
    ILogger<KafkaConsumerWorker> logger,
    IConfiguration configuration,
    UserRegisteredEventHandler handler
) : IEventConsumer
{
    private readonly ILogger<KafkaConsumerWorker> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserRegisteredEventHandler _handler = handler;

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(" 🫟[KAFKA_INFRA] Iniciando processo");

        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"]
                               ?? throw new InvalidOperationException("Kafka:BootstrapServers não configurado"),

            GroupId = _configuration["Kafka:GroupId"] ?? "consumer-worker",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        var topic = nameof(UserRegisteredEvent);
        consumer.Subscribe(topic);

        _logger.LogInformation(" ✅[KAFKA] Consumindo tópico - ♻️ {Topic}", topic);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);

                var evt = JsonSerializer.Deserialize<UserRegisteredEvent>(result.Message.Value)!;

                await _handler.HandleAsync(evt, cancellationToken);

                consumer.Commit(result);
            }
        }
        catch (OperationCanceledException)
        {
            // shutdown limpo
        }
        finally
        {
            consumer.Close();
        }
    }
}