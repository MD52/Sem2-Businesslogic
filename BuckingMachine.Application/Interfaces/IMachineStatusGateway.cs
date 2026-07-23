namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Enums;

public interface IMachineStatusGateway
{
    Task<MotionStates> ReadStatusAsync(CancellationToken cancellationToken = default);
}
