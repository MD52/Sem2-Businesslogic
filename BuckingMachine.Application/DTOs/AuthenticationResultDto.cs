namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class AuthenticationResultDto
{
    public bool IsAuthenticated { get; init; }
    public string? AccessToken { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public int? OperatorId { get; init; }
    public string? Username { get; init; }
    public string? DisplayName { get; init; }
    public OperatorRole? Role { get; init; }
}
