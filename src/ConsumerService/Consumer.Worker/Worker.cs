using Consumer.Application.Ports.Messaging;

namespace Consumer.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IServiceProvider serviceProvider,
        ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Bootstrap
        _logger.LogInformation("🚀 Worker iniciado");

        using var scope = _serviceProvider.CreateScope();

        var consumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
        await consumer.ConsumeAsync(stoppingToken);
    }
}