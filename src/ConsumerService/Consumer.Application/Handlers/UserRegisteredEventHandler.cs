using Consumer.Application.Ports.Persistence;
using Consumer.Domain.Entities;
using Shared.Contracts.Events;

namespace Consumer.Application.Handlers;

public class UserRegisteredEventHandler(IProcessingResultRepository repository, IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProcessingResultRepository _repository = repository;

    public async Task HandleAsync(UserRegisteredEvent @event, CancellationToken ct)
    {
        var result = new UserProcessingResult(@event);

        await _repository.Insert(result);
        await _unitOfWork.SaveChangesAsync();
    }
}