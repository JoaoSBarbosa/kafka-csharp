namespace Producer.Application.Ports.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}