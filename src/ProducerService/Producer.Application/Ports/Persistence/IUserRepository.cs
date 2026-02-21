using Producer.Domain.Entities;

namespace Producer.Application.Ports.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user);

}