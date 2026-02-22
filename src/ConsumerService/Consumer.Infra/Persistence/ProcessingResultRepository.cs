using Consumer.Application.Ports.Persistence;
using Consumer.Domain.Entities;
using Consumer.Infra.Data.Context;

namespace Consumer.Infra.Persistence;

public class ProcessingResultRepository(ConsumerContext context) : IProcessingResultRepository
{
    private readonly ConsumerContext _context = context;

    public Task Insert(UserProcessingResult result)
    {
        _context.UserProcessingResults.Add(result);
        return Task.CompletedTask;
    }
}