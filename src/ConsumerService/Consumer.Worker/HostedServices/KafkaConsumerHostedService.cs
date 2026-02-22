using Consumer.Application.Ports.Messaging;

namespace Consumer.Worker.HostedServices;

public class KafkaConsumerHostedService(
    ILogger<KafkaConsumerHostedService> logger,
    IEventConsumer consumer
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("[WORKER] Consumer iniciado");
        await consumer.ConsumeAsync(stoppingToken);
    }
}