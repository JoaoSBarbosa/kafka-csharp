using Shared.Contracts.Topics;

namespace Shared.Contracts.Abstractions;

public interface IKafkaEndpoint
{
    KafkaTopics Topic { get; }
}