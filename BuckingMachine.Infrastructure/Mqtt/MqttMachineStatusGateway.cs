namespace BuckingMachine.Infrastructure.Mqtt;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Enums;

public sealed class MqttMachineStatusGateway : IMachineStatusGateway
{
    public Task<MotionState> ReadStatusAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
