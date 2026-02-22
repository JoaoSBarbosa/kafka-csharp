using Shared.Contracts.Events;

namespace Consumer.Domain.Entities;

public class UserProcessingResult
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    public UserProcessingResult()
    {
    }

    public UserProcessingResult(UserRegisteredEvent @event)
    {
        Id = Guid.NewGuid();
        UserId = @event.UserId;
        Email = @event.Email;
        Success = true;
        ProcessedAt = DateTime.Now;
    }

    public void MarkAsFailed(string error)
    {
        Success = false;
        ErrorMessage = error;
    }
}