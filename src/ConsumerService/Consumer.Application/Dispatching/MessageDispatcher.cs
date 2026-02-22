using System.Text.Json;
using Consumer.Application.DTOs;
using Consumer.Application.Features.UserProcessings.Interfaces;
using Consumer.Application.Ports.Messaging;
using Shared.Contracts.Events;

namespace Consumer.Application.Dispatching;

public class MessageDispatcher(IUserProcessingResultService userProcessingResultService) : IMessageDispatcher
{
    private readonly IUserProcessingResultService _userProcessingResultService = userProcessingResultService;

    public async Task<DispatchResult> DispatchAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            var evt = JsonSerializer.Deserialize<UserRegisteredEvent>(message);
            if (evt is null) return new(false, false, "Evento inválido");

            await _userProcessingResultService.Insert(evt, cancellationToken);
            return new(true, false, null);
        }
        catch (JsonException ex)
        {
            return new(false, false, ex.Message);
        }
        catch (Exception ex)
        {
            return new(false, true, ex.Message);
        }
    }
}