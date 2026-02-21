namespace Producer.Application.Users.DTOs;

public record UserResponse(Guid Id, string FirstName, string LastName, string Email);