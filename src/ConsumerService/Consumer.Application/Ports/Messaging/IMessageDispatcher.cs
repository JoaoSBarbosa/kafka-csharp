using Consumer.Application.DTOs;

namespace Consumer.Application.Ports.Messaging;

public interface IMessageDispatcher
{
    Task<DispatchResult> DispatchAsync(string message, CancellationToken cancellationToken);
}