using Consumer.Application.Ports.Persistence;
using Consumer.Infra.Data.Context;

namespace Consumer.Infra.Persistence;

public class UnitOfWork(ConsumerContext context) : IUnitOfWork
{
    private readonly ConsumerContext _context = context;

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}