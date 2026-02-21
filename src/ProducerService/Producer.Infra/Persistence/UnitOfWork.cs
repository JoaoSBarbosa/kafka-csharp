using Producer.Application.Ports.Persistence;
using Producer.Infra.Context;

namespace Producer.Infra.Persistence;

public class UnitOfWork(KafkaPlaygroundContext context) : IUnitOfWork
{
    private readonly KafkaPlaygroundContext _context = context;

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}