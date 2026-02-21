using Producer.Domain.Entities;

namespace Producer.Application.Ports.Persistence;

public interface IUserRepository
{
    void Insert(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task Update(User user);
    void DeleteAsync(User user);
}