using Consumer.Application.Ports.Messaging;
using Shared.Contracts.Events;

namespace Consumer.Infra.Kafka.Consumers;

public class KafkaConsumerWorker : IEventConsumer<UserRegisteredEvent>
{
    public Task ConsumerAsync(UserRegisteredEvent @event)
    {
        throw new NotImplementedException();
    }
}