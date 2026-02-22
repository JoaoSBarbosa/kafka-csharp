using Consumer.Application.Dispatching;
using Consumer.Application.Features.UserProcessings.Interfaces;
using Consumer.Application.Features.UserProcessings.Services;
using Consumer.Application.Ports.Messaging;
using Consumer.Application.Ports.Persistence;
using Consumer.Infra.Kafka.Consumer;
using Consumer.Infra.Persistence;

namespace Consumer.Worker.Config;

public static class WorkerConfig
{
    public static IServiceCollection AddWorkerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProcessingResultRepository, ProcessingResultRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserProcessingResultService, UserProcessingResultService>();
        services.AddScoped<IMessageDispatcher, MessageDispatcher>();
        services.AddScoped<IEventConsumer, KafkaConsumerWorker>();

        return services;
    }
}