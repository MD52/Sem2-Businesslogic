namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Enums;

public interface IMachineStatusGateway
{
    Task<MotionState> ReadStatusAsync(CancellationToken cancellationToken = default);
}
