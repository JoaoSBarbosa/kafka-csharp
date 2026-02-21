using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Ports.Messaging;
using Producer.Application.Ports.Persistence;
using Producer.Infra.Persistence;

namespace Producer.Infra.Config;

public static class FakeInfraModule
{
    public static IServiceCollection AddFakeInfra(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, InMemoryUserRepository>();
        services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();
        return services;
    }
}