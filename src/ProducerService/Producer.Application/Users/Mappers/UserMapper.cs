using Producer.Application.Users.DTOs;
using Producer.Domain.Entities;

namespace Producer.Application.Users.Mappers;

public static class UserMapper
{
    public static UserResponse ToUserResponse(User user) =>
        new UserResponse(user.Id, user.FistName, user.LastName, user.Email);

    public static User ToEntity(RegisterUserRequest request) =>
        new User(request.FirstName, request.LastName, request.Email);
}