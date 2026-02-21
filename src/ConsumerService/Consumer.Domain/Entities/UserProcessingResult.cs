namespace Consumer.Domain.Entities;

public class UserProcessingResult
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    public UserProcessingResult()
    {
    }

    public UserProcessingResult(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Success = true;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed( string error)
    {
        Success = false;
        ErrorMessage = error;
    }
}