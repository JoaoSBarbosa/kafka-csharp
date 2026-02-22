using Shared.Contracts.Topics;

namespace Shared.Contracts.Abstractions;

public interface IIntegrationEvent
{
    KafkaTopics Topic { get; }
}