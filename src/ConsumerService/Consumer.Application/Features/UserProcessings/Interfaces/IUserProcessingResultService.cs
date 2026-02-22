using Consumer.Domain.Entities;
using Shared.Contracts.Events;

namespace Consumer.Application.Features.UserProcessings.Interfaces;

public interface IUserProcessingResultService
{
    Task Insert(UserRegisteredEvent @event, CancellationToken cancellationToken);
    Task<IEnumerator<UserProcessingResult>> FindAll();
    Task<UserProcessingResult> FindByUserEmail(string email);
    Task<UserProcessingResult> FindById(Guid id);
}