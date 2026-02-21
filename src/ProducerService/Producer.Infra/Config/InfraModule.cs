using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Producer.Application.Ports.Messaging;
using Producer.Application.Ports.Persistence;
using Producer.Infra.Context;
using Producer.Infra.Kafka.Producers;
using Producer.Infra.Persistence;

namespace Producer.Infra.Config;

public static class InfraModule
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        var connection = configuration.GetConnectionString("DefaultConnection")
                         ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não configurada");
        services.AddDbContext<KafkaPlaygroundContext>(options => options.UseSqlServer(connection));

        // Repositórios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Kafka Usando factory para passar Logger
        services.AddSingleton<IEventPublisher>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<KafkaEventPublisher>>();
            var bootstrapServers = configuration.GetSection("Kafka:BootstrapServers").Value
                                   ?? throw new InvalidOperationException("Kafka:BootstrapServers não configurado");
            return new KafkaEventPublisher(bootstrapServers, logger);
        });

        return services;
    }
}