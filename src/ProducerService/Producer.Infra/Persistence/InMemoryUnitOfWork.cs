using Producer.Application.Ports.Persistence;

namespace Producer.Infra.Persistence;

public class InMemoryUnitOfWork : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await Task.CompletedTask;
    }
}