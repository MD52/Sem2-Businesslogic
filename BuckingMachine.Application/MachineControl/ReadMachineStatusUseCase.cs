namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;

public sealed class ReadMachineStatusUseCase
{
    private readonly IMachineControlGateway _gateway;

    public ReadMachineStatusUseCase(IMachineControlGateway gateway) => _gateway = gateway;

    public async Task<MachineStatusDto> ExecuteAsync()
    {
        MachineSimulationStatus status = await LoadMachineStatusAsync();
        return MapToDto(status);
    }

    private Task<MachineSimulationStatus> LoadMachineStatusAsync() => _gateway.ReadStateAsync();

    private static MachineStatusDto MapToDto(MachineSimulationStatus status) => new()
    {
        MotionState = status.MotionState,
        CompletedCycles = status.CompletedCycles,
        CurrentParameters = status.CurrentParameters is null ? null : DtoMapper.Map(status.CurrentParameters)
    };
}
