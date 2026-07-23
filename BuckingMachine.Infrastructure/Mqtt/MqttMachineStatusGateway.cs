namespace BuckingMachine.Infrastructure.Mqtt;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Enums;

public sealed class MqttMachineStatusGateway : IMachineStatusGateway
{
    public Task<MotionStates> ReadStatusAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
