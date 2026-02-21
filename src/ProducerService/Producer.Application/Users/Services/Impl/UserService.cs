using Producer.Application.Exceptions;
using Producer.Application.Ports.Messaging;
using Producer.Application.Ports.Persistence;
using Producer.Application.Users.DTOs;
using Producer.Application.Users.Mappers;
using Producer.Domain.Entities;
using Shared.Contracts.Events;

namespace Producer.Application.Users.Services.Impl;

public class UserService(
    IUserRepository repository,
    IEventPublisher eventPublisher,
    IUnitOfWork unitOfWork
) : IUserService
{
    private readonly IUserRepository _userRepository = repository;
    private readonly IEventPublisher _eventPublisher = eventPublisher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(RegisterUserRequest request)
    {
        ValidateUser(request);
        var user = UserMapper.ToEntity(request);
        _userRepository.Insert(user);

        await PublishUserRegisteredEventAsync(user);
        user.MarkAsSent();

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<UserResponse> FindByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId) ??
                   throw new EntityNotFoundException($"Usuário {userId} não encontrado");
        return UserMapper.ToUserResponse(user);
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(UserMapper.ToUserResponse);
    }

    private async Task PublishUserRegisteredEventAsync(User user)
    {
        var @event = new UserRegisteredEvent(user.Id, user.Email, DateTime.Now);
        await _eventPublisher.PublishAsync(@event);
    }


    private void ValidateUser(RegisterUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("O email deve ser informado.", nameof(request.Email));

        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new ArgumentException("O primeiro nome deve ser informado.", nameof(request.FirstName));

        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new ArgumentException("O segundo nome deve ser informado.", nameof(request.LastName));
    }
}