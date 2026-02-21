using Producer.Application.Users.DTOs;

namespace Producer.Application.Users.Services;

public interface IUserService
{
    Task AddAsync(UserCreate create);
    Task<UserResponse> FindByIdAsync(Guid userId);
    Task<IEnumerable<UserResponse>> GetAllAsync();
}