using Producer.Application.Ports.Persistence;
using Producer.Domain.Entities;

namespace Producer.Infra.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_users.SingleOrDefault(u => u.Id == id));
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task UpdateAsync(User user)
    {
        var index = _users.FindIndex(u => u.Id == user.Id);
        if (index >= 0)
        {
            _users[index] = user;
        }
        else
        {
            _users.Add(user);
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        _users.RemoveAll(u => u.Id == user.Id);
        return Task.CompletedTask;
    }
}