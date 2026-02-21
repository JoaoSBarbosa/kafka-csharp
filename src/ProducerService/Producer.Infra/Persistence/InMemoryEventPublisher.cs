using Producer.Application.Ports.Messaging;

namespace Producer.Infra.Persistence;

public class InMemoryEventPublisher : IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
    {
        Console.WriteLine($"[EVENT PUBLISHED] {@event}");
        return Task.CompletedTask;
    }
}