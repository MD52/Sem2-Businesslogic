namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Application.MachineControl;
using BuckingMachine.Domain.Entities;

public interface IMachineControlGateway
{
    Task StartSingleCycleAsync(CancellationToken cancellationToken = default);
    Task StartContinuousAsync(CancellationToken cancellationToken = default);
    Task StopAsync();
    Task ResetAsync();
    Task TriggerFaultAsync();
    Task UpdateParametersAsync(ParameterData parameterData);
    Task<MachineSimulationStatus> ReadStateAsync();
}
