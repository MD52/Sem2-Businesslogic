namespace BuckingMachine.Infrastructure.Mqtt;

using BuckingMachine.Application.Interfaces;

public sealed class MqttMachineCommandGateway : IMachineCommandGateway
{
    public Task SendStartCommandAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SendStopCommandAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
