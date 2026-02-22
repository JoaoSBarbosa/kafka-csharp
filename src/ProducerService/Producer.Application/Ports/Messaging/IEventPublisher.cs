using Shared.Contracts.Abstractions;
using Shared.Contracts.Topics;

namespace Producer.Application.Ports.Messaging;

public interface IEventPublisher : IKafkaEndpoint
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
}