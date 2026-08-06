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

    public Task ExecuteAsync(
        ParameterData currentParameterData,
        StatusData statusData,
        MachineCycle machineCycle,
        CancellationToken cancellationToken = default)
    {
        ValidateData(currentParameterData, statusData, machineCycle);

        var cycle = new MachineCycle
        {
            MachineId = machineCycle.MachineId,
            ParameterDataId = currentParameterData.ParameterDataId,
            Name = machineCycle.Name,
            StartTime = machineCycle.StartTime,
            EndTime = machineCycle.EndTime,
            Duration = machineCycle.Duration
        };

        return _repository.SaveCompletedCycleAsync(cycle, statusData, cancellationToken);
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

    private static void ValidateData(ParameterData parameterData, StatusData statusData, MachineCycle cycle)
    {
        if (parameterData.ParameterDataId <= 0)
            throw new ArgumentException("Der Parametersatz muss bereits gespeichert sein.", nameof(parameterData));
        if (cycle.MachineId <= 0 || statusData.MachineId != cycle.MachineId || parameterData.MachineId != cycle.MachineId)
            throw new ArgumentException("Alle Datensaetze muessen zur selben Maschine gehoeren.");
        if (statusData.Timestamp == default)
            throw new ArgumentException("Der Statuszeitpunkt muss angegeben werden.", nameof(statusData));
        if (cycle.StartTime == default || cycle.EndTime is null)
            throw new ArgumentException("Nur abgeschlossene Zyklen koennen gespeichert werden.", nameof(cycle));
        if (cycle.EndTime < cycle.StartTime)
            throw new ArgumentException("Das Zyklusende darf nicht vor dem Start liegen.", nameof(cycle));
    }
}
