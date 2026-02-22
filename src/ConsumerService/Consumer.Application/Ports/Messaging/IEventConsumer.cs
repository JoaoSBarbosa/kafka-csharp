using Shared.Contracts.Abstractions;

namespace Consumer.Application.Ports.Messaging;

public interface IEventConsumer : IKafkaEndpoint
{
    Task ConsumeAsync(CancellationToken cancellationToken);
}