namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Application.MachineControl;
using BuckingMachine.Domain.Entities;

public interface IMachineControlGateway
{
    Task StartSingleCycleAsync(CancellationToken cancellationToken = default);
    Task StartContinuousAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
    Task ResetAsync(CancellationToken cancellationToken = default);
    Task<MachineSimulationStatus> ReadStateAsync(CancellationToken cancellationToken = default);
    Task UpdateParametersAsync(ParameterData parameterData, CancellationToken cancellationToken = default);
}
