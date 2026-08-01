namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Enums;

public sealed class StopMachineUseCase
{
    private readonly IMachineStatusGateway _machineStatusGateway;
    private readonly IMachineCommandGateway _machineCommandGateway;

    public StopMachineUseCase(
        IMachineStatusGateway machineStatusGateway,
        IMachineCommandGateway machineCommandGateway)
    {
        _machineStatusGateway = machineStatusGateway;
        _machineCommandGateway = machineCommandGateway;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var machineState = await _machineStatusGateway.ReadStatusAsync(cancellationToken);
        ValidateMachineCanStop(machineState);
        await _machineCommandGateway.SendStopCommandAsync(cancellationToken);
    }

    private static void ValidateMachineCanStop(MotionState motionState)
    {
        if (motionState == MotionState.Idle)
        {
            throw new InvalidOperationException("Die Maschine ist bereits gestoppt.");
        }
    }
}
