namespace BuckingMachine.Application.ProcessData;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class RecordProcessDataUseCase
{
    private readonly IProcessDataRepository _repository;
    private readonly IMachineControlGateway _machineControlGateway;

    public RecordProcessDataUseCase(
        IProcessDataRepository repository,
        IMachineControlGateway machineControlGateway)
    {
        _repository = repository;
        _machineControlGateway = machineControlGateway;
    }

    public async Task SaveParameterDataAsync(
        ParameterData parameterData,
        CancellationToken cancellationToken = default)
    {
        ValidateParameters(parameterData);
        await _machineControlGateway.UpdateParametersAsync(parameterData);
        await _repository.SaveParameterDataAsync(parameterData, cancellationToken);
    }

    private static void ValidateParameters(ParameterData parameterData)
    {
        ArgumentNullException.ThrowIfNull(parameterData);

        if (parameterData.MachineId <= 0)
            throw new ArgumentException("Die MachineId muss groesser als 0 sein.", nameof(parameterData));
        if (parameterData.RecordedAt == default)
            throw new ArgumentException("Der Speicherzeitpunkt muss angegeben werden.", nameof(parameterData));
        if (parameterData.AmountCycleMovements <= 0)
            throw new ArgumentException("Die Anzahl Zyklusbewegungen muss groesser als 0 sein.", nameof(parameterData));
    }
}
