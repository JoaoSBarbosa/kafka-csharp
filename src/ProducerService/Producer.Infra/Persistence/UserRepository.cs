using Microsoft.EntityFrameworkCore;
using Producer.Application.Ports.Persistence;
using Producer.Domain.Entities;
using Producer.Infra.Context;

namespace Producer.Infra.Persistence;

public class UserRepository(KafkaPlaygroundContext context) : IUserRepository
{
    private readonly KafkaPlaygroundContext _context = context;

    public void Insert(User user)
    {
        _context.User.Add(user);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.AsNoTracking().ToListAsync();
    }

    public Task Update(User user)
    {
        _context.User.Update(user);
        return Task.CompletedTask;
    }

    public void DeleteAsync(User user)
    {
        _context.User.Remove(user);
    }
}