using Consumer.Domain.Entities;

namespace Consumer.Application.Ports.Persistence;

public interface IProcessingResultRepository
{
    Task Insert(UserProcessingResult result);
}