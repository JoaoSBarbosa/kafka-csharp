using Consumer.Application.Features.UserProcessings.Interfaces;
using Consumer.Application.Ports.Persistence;
using Consumer.Domain.Entities;
using Shared.Contracts.Events;

namespace Consumer.Application.Features.UserProcessings.Services;

public class UserProcessingResultService(IProcessingResultRepository repository, IUnitOfWork unitOfWork)
    : IUserProcessingResultService
{
    private readonly IProcessingResultRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Insert(UserRegisteredEvent @event, CancellationToken cancellationToken)
    {
        var result = new UserProcessingResult(@event);
        await _repository.Insert(result);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<IEnumerator<UserProcessingResult>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserProcessingResult> FindByUserEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserProcessingResult> FindById(Guid id)
    {
        throw new NotImplementedException();
    }
}