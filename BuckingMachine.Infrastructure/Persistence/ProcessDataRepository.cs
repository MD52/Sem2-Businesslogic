namespace BuckingMachine.Infrastructure.Persistence;

using System.Collections.Concurrent;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class ProcessDataRepository : IProcessDataRepository
{
    private readonly ConcurrentDictionary<int, ParameterData> _parameters = new();
    private int _nextParameterId;

    public Task<int> SaveParameterDataAsync(
        ParameterData parameterData,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int id = parameterData.ParameterDataId > 0
            ? parameterData.ParameterDataId
            : Interlocked.Increment(ref _nextParameterId);

        _parameters[id] = Copy(parameterData, id);
        return Task.FromResult(id);
    }

    private static ParameterData Copy(ParameterData source, int id) => new()
    {
        ParameterDataId = id,
        MachineId = source.MachineId,
        RecordedAt = source.RecordedAt,
        OperationModeSideDrives = source.OperationModeSideDrives,
        TargetVelocitySideDrives = source.TargetVelocitySideDrives,
        TargetTorqueSideDrives = source.TargetTorqueSideDrives,
        TargetPosSideDrives = source.TargetPosSideDrives,
        OperationModeMainDrives = source.OperationModeMainDrives,
        TargetVelocityMainDrives = source.TargetVelocityMainDrives,
        TargetTorqueMainDrives = source.TargetTorqueMainDrives,
        TargetPosMainDrives = source.TargetPosMainDrives,
        BreakTimeHoldPos = source.BreakTimeHoldPos,
        ReleaseTimeHoldPos = source.ReleaseTimeHoldPos,
        AmountCycleMovements = source.AmountCycleMovements
    };

}
