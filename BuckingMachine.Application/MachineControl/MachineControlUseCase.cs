namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;

public sealed class MachineControlUseCase
{
    private readonly IMachineControlGateway _gateway;

    public MachineControlUseCase(IMachineControlGateway gateway) => _gateway = gateway;

    public Task StartCycleAsync(CancellationToken cancellationToken = default) =>
        _gateway.StartSingleCycleAsync(cancellationToken);

    public Task StartContinuousAsync(CancellationToken cancellationToken = default) =>
        _gateway.StartContinuousAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken = default) =>
        _gateway.StopAsync(cancellationToken);

    public Task ResetAsync(CancellationToken cancellationToken = default) =>
        _gateway.ResetAsync(cancellationToken);

    public async Task<MachineStatusDto> ReadStateAsync(CancellationToken cancellationToken = default)
    {
        MachineSimulationStatus status = await _gateway.ReadStateAsync(cancellationToken);

        return new MachineStatusDto
        {
            MotionState = status.MotionState,
            CompletedCycles = status.CompletedCycles,
            CurrentParameters = status.CurrentParameters is null
                ? null
                : DtoMapper.Map(status.CurrentParameters)
        };
    }
}
