namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class ProcessDataRepository : IProcessDataRepository
{
    public Task SaveProcessDataAsync(
        ProcessData processData,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MachineCycle> GetCycleDataAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ProcessData>> GetProcessDataAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(
        Guid machineId,
        DateTime? from,
        DateTime? to,
        int? limit,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
