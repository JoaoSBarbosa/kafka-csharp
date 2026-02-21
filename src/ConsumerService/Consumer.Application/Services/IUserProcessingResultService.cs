using Consumer.Domain.Entities;
using Shared.Contracts.Events;

namespace Consumer.Application.Services;

public interface IUserProcessingResultService
{
    Task Create(UserRegisteredEvent @event);
}