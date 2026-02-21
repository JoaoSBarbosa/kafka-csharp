namespace Shared.Contracts.Events;

public record UserRegisteredEvent(Guid UserId, string Email, DateTime OccuredAt);