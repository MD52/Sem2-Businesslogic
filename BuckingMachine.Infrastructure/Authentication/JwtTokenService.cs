namespace BuckingMachine.Infrastructure.Authentication;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;

public sealed class JwtTokenService : IAuthenticationService
{
    public Task<AuthenticationResultDto> AuthenticateAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
