namespace Consumer.Application.Ports.Messaging;

public interface IEventConsumer<TEvent>
{
    Task ConsumerAsync(TEvent @event);
}