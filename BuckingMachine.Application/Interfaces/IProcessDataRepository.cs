namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Entities;

public interface IProcessDataRepository
{
    Task SaveProcessDataAsync(
        ProcessData processData,
        CancellationToken cancellationToken = default);

    Task<MachineCycle> GetCycleDataAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ProcessData>> GetProcessDataAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(
        Guid machineId,
        DateTime? from,
        DateTime? to,
        int? limit,
        CancellationToken cancellationToken = default);
}

