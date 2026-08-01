namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Enums;  



public sealed class ReadMachineStatusUseCase
{
    private readonly IMachineStatusGateway _machineStatusGateway;

    public ReadMachineStatusUseCase(
        IMachineStatusGateway machineStatusGateway)
    {
        _machineStatusGateway = machineStatusGateway;
    }

    public async Task<MachineStatusDto> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var motionState = await LoadMachineStatusAsync(cancellationToken);

        return MapToDto(motionState);
    }

    private async Task<MotionState> LoadMachineStatusAsync(
        CancellationToken cancellationToken)
    {
        return await _machineStatusGateway.ReadStatusAsync(cancellationToken);
    }

    private static MachineStatusDto MapToDto(
        MotionState motionState)
    {
        return new MachineStatusDto
        {
            MotionState = motionState
        };
    }
}
