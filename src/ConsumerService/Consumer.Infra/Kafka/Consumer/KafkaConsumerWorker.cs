using Confluent.Kafka;
using Consumer.Application.Ports.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Contracts.Topics;

namespace Consumer.Infra.Kafka.Consumer;

public class KafkaConsumerWorker(
    ILogger<KafkaConsumerWorker> logger,
    IConfiguration configuration,
    IMessageDispatcher dispatcher
) : IEventConsumer
{
    private readonly ILogger<KafkaConsumerWorker> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly IMessageDispatcher _dispatcher = dispatcher;

    public KafkaTopics Topic => KafkaTopics.Registered;

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        var config = GetConsumerConfig();
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(Topic.Name);

        _logger.LogInformation("📥 Consumindo tópico {Topic}", Topic.Name);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);

                var dispatchResult = await _dispatcher.DispatchAsync(result.Message.Value, cancellationToken);

                if (dispatchResult.Success)
                {
                    consumer.Commit(result);
                }
                else
                {
                    _logger.LogWarning(
                        "Mensagem descartada. Erro={Error}",
                        dispatchResult.ErrorMessage
                    );

                    consumer.Commit(result);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Consumer encerrado");
        }
        finally
        {
            consumer.Close();
        }
    }

    private ConsumerConfig GetConsumerConfig()
    {
        return new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"] ??
                               throw new InvalidOperationException("Kafka:BootstrapServers não configurado"),

            GroupId = _configuration["Kafka:GroupId"] ?? "consumer-worker",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
    }
}