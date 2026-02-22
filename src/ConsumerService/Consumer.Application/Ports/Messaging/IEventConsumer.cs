namespace Consumer.Application.Ports.Messaging;

public interface IEventConsumer
{
    Task ConsumeAsync(CancellationToken cancellationToken);
}