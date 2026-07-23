namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Application.DTOs;

public interface IAuthenticationService
{
    Task<AuthenticationResultDto> AuthenticateAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default);
}
