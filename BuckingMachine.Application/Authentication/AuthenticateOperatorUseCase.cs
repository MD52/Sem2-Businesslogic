namespace BuckingMachine.Application.Authentication;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;

public sealed class AuthenticateOperatorUseCase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticateOperatorUseCase(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public Task<AuthenticationResultDto> ExecuteAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        return _authenticationService.AuthenticateAsync(
            userName,
            password,
            cancellationToken);
    }
}
