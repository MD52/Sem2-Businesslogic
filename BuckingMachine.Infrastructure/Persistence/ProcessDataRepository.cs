namespace BuckingMachine.Infrastructure.Persistence;

using System.Collections.Concurrent;
using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class ProcessDataRepository : IProcessDataRepository
{
    private readonly ConcurrentDictionary<int, ParameterData> _parameters = new();
    private readonly ConcurrentDictionary<int, StatusData> _statuses = new();
    private readonly ConcurrentDictionary<int, MachineCycle> _cycles = new();
    private int _nextParameterId;
    private int _nextStatusId;
    private int _nextCycleId;

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

    public Task<int> SaveStatusDataAsync(
        StatusData statusData,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int id = statusData.StatusDataId > 0
            ? statusData.StatusDataId
            : Interlocked.Increment(ref _nextStatusId);

        _statuses[id] = Copy(statusData, id);
        return Task.FromResult(id);
    }

    public Task<int> SaveMachineCycleAsync(
        MachineCycle machineCycle,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int id = machineCycle.CycleId > 0
            ? machineCycle.CycleId
            : Interlocked.Increment(ref _nextCycleId);

        _cycles[id] = Copy(machineCycle, id);
        return Task.FromResult(id);
    }

    public async Task SaveCompletedCycleAsync(
        MachineCycle machineCycle,
        StatusData statusData,
        CancellationToken cancellationToken = default)
    {
        int statusId = await SaveStatusDataAsync(statusData, cancellationToken);
        var completedCycle = Copy(machineCycle, machineCycle.CycleId, statusId);
        await SaveMachineCycleAsync(completedCycle, cancellationToken);
    }

    public async Task<MachineCycleDto?> GetCycleDataAsync(
        int cycleId,
        CancellationToken cancellationToken = default)
    {
        MachineCycle? cycle = await GetMachineCycleAsync(cycleId, cancellationToken);
        if (cycle is null)
            return null;

        ParameterData? parameters = await GetParameterDataAsync(cycle.ParameterDataId, cancellationToken);
        StatusData? status = await GetStatusDataAsync(cycle.StatusDataId, cancellationToken);

        return new MachineCycleDto
        {
            CycleId = cycle.CycleId,
            MachineId = cycle.MachineId,
            ParameterDataId = cycle.ParameterDataId,
            StatusDataId = cycle.StatusDataId,
            Name = cycle.Name,
            StartTime = cycle.StartTime,
            EndTime = cycle.EndTime,
            Duration = cycle.Duration,
            ParameterData = parameters is null ? null : Map(parameters),
            StatusData = status is null ? null : Map(status)
        };
    }

    public Task<MachineCycle?> GetMachineCycleAsync(int cycleId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _cycles.TryGetValue(cycleId, out MachineCycle? cycle);
        return Task.FromResult(cycle);
    }

    public Task<ParameterData?> GetParameterDataAsync(int parameterDataId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _parameters.TryGetValue(parameterDataId, out ParameterData? parameterData);
        return Task.FromResult(parameterData);
    }

    public Task<StatusData?> GetStatusDataAsync(int statusDataId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _statuses.TryGetValue(statusDataId, out StatusData? statusData);
        return Task.FromResult(statusData);
    }

    public Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(
        DateTime? from,
        DateTime? to,
        int? limit,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<MachineCycle> query = _cycles.Values;

        if (from.HasValue)
            query = query.Where(cycle => cycle.StartTime >= from.Value);
        if (to.HasValue)
            query = query.Where(cycle => cycle.StartTime <= to.Value);

        query = query.OrderByDescending(cycle => cycle.StartTime);
        if (limit.HasValue)
            query = query.Take(limit.Value);

        return Task.FromResult<IReadOnlyCollection<MachineCycle>>(query.ToArray());
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

    private static StatusData Copy(StatusData source, int id) => new()
    {
        StatusDataId = id,
        MachineId = source.MachineId,
        Timestamp = source.Timestamp,
        MotionState = source.MotionState,
        OperationModeSideDrives = source.OperationModeSideDrives,
        ActualVelocitySideDrives = source.ActualVelocitySideDrives,
        ActualTorqueSideDrives = source.ActualTorqueSideDrives,
        ActualPosSideDrives = source.ActualPosSideDrives,
        OperationModeMainDrives = source.OperationModeMainDrives,
        ActualVelocityMainDrives = source.ActualVelocityMainDrives,
        ActualTorqueMainDrives = source.ActualTorqueMainDrives,
        ActualPosMainDrives = source.ActualPosMainDrives
    };

    private static MachineCycle Copy(MachineCycle source, int id, int? statusId = null) => new()
    {
        CycleId = id,
        MachineId = source.MachineId,
        ParameterDataId = source.ParameterDataId,
        StatusDataId = statusId ?? source.StatusDataId,
        Name = source.Name,
        StartTime = source.StartTime,
        EndTime = source.EndTime,
        Duration = source.Duration,
        ParameterData = source.ParameterData,
        StatusData = source.StatusData
    };

    private static ParameterDataDto Map(ParameterData p) => new()
    {
        ParameterDataId=p.ParameterDataId, MachineId=p.MachineId, RecordedAt=p.RecordedAt,
        OperationModeSideDrives=p.OperationModeSideDrives, TargetVelocitySideDrives=p.TargetVelocitySideDrives,
        TargetTorqueSideDrives=p.TargetTorqueSideDrives, TargetPosSideDrives=p.TargetPosSideDrives,
        OperationModeMainDrives=p.OperationModeMainDrives, TargetVelocityMainDrives=p.TargetVelocityMainDrives,
        TargetTorqueMainDrives=p.TargetTorqueMainDrives, TargetPosMainDrives=p.TargetPosMainDrives,
        BreakTimeHoldPos=p.BreakTimeHoldPos, ReleaseTimeHoldPos=p.ReleaseTimeHoldPos,
        AmountCycleMovements=p.AmountCycleMovements
    };

    private static StatusDataDto Map(StatusData s) => new()
    {
        StatusDataId=s.StatusDataId, MachineId=s.MachineId, Timestamp=s.Timestamp, MotionState=s.MotionState,
        OperationModeSideDrives=s.OperationModeSideDrives, ActualVelocitySideDrives=s.ActualVelocitySideDrives,
        ActualTorqueSideDrives=s.ActualTorqueSideDrives, ActualPosSideDrives=s.ActualPosSideDrives,
        OperationModeMainDrives=s.OperationModeMainDrives, ActualVelocityMainDrives=s.ActualVelocityMainDrives,
        ActualTorqueMainDrives=s.ActualTorqueMainDrives, ActualPosMainDrives=s.ActualPosMainDrives
    };
}
