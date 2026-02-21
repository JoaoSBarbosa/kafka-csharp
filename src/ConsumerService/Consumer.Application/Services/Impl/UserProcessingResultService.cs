using Consumer.Application.Ports.Persistence;
using Consumer.Domain.Entities;
using Shared.Contracts.Events;

namespace Consumer.Application.Services.Impl;

public class UserProcessingResultService(IProcessingResultRepository repository, IUnitOfWork unitOfWork)
    : IUserProcessingResultService
{
    private readonly IProcessingResultRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Create(UserRegisteredEvent @event)
    {
        var result = new UserProcessingResult(@event);
        await _repository.AddAsync(result);
        await _unitOfWork.SaveChangesAsync();
    }
}