namespace Consumer.Application.DTOs;

public record DispatchResult(bool Success, bool Retry, string? ErrorMessage);