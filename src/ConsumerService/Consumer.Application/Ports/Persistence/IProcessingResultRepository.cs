using Consumer.Domain.Entities;

namespace Consumer.Application.Ports.Persistence;

public interface IProcessingResultRepository
{
    Task AddAsync(UserProcessingResult result);
}