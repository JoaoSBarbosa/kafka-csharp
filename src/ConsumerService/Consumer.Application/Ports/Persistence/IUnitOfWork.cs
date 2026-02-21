namespace Consumer.Application.Ports.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}