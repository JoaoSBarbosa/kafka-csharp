namespace Consumer.Application.Ports.Messaging;

public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}