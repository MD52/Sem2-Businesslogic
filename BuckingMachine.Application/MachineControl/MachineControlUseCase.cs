namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Application.Interfaces;

public sealed class MachineControlUseCase
{
    private readonly IMachineControlGateway _gateway;

    public MachineControlUseCase(IMachineControlGateway gateway) => _gateway = gateway;

    public Task StartCycleAsync(CancellationToken cancellationToken = default) =>
        _gateway.StartSingleCycleAsync(cancellationToken);

    public Task StartContinuousAsync(CancellationToken cancellationToken = default) =>
        _gateway.StartContinuousAsync(cancellationToken);

    public Task StopAsync() => _gateway.StopAsync();

    public Task ResetAsync() => _gateway.ResetAsync();

    public Task TriggerFaultAsync() => _gateway.TriggerFaultAsync();
}
