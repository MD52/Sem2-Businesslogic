namespace BuckingMachine.Application.Interfaces;

public interface IMachineCommandGateway
{
    Task SendStartCommandAsync(CancellationToken cancellationToken = default);
    Task SendStopCommandAsync(CancellationToken cancellationToken = default);
}
