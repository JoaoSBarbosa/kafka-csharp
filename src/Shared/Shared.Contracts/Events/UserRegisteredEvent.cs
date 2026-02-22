using Shared.Contracts.Abstractions;
using Shared.Contracts.Topics;

namespace Shared.Contracts.Events;

public record UserRegisteredEvent(
    Guid UserId,
    string Email,
    DateTime OccuredAt
);