namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;

public sealed class ReadMachineStatusUseCase
{
    private readonly IMachineStatusGateway _machineStatusGateway;

    public ReadMachineStatusUseCase(IMachineStatusGateway machineStatusGateway)
    {
        _machineStatusGateway = machineStatusGateway;
    }

    public async Task<MachineStatusDto> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var motionState = await _machineStatusGateway.ReadStatusAsync(cancellationToken);
        return new MachineStatusDto { MotionState = motionState };
    }
}
