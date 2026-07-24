using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Enums;

namespace BuckingMachine.Application.MachineControl;

public sealed class StartMachineUseCase
{
    private readonly IMachineStatusGateway _machineStatusGateway;
    private readonly IMachineCommandGateway _machineCommandGateway;

    public StartMachineUseCase(
        IMachineStatusGateway machineStatusGateway,
        IMachineCommandGateway machineCommandGateway)
    {
        _machineStatusGateway = machineStatusGateway;
        _machineCommandGateway = machineCommandGateway;
    }




    public async Task ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var machineState =
            await _machineStatusGateway.ReadStatusAsync(cancellationToken);
        ValidateMachineCanStart(machineState);

        await _machineCommandGateway.SendStartCommandAsync(
            cancellationToken);
    }



    private static void ValidateMachineCanStart(
        MotionStates motionState)
    {
        if (motionState != MotionStates.Idle)
        {
            throw new InvalidOperationException(
                "Die Maschine ist nicht startbereit.");
        }
    }
}
