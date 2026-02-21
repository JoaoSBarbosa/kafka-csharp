using Producer.Application.Users.DTOs;

namespace Producer.Application.Users.Services;

public interface IUserService
{
    Task AddAsync(RegisterUserRequest request);
    Task<UserResponse> FindByIdAsync(Guid userId);
    Task<IEnumerable<UserResponse>> GetAllAsync();
}