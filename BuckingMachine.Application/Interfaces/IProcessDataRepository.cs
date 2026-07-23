namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Entities;

public interface IProcessDataRepository
{
    Task AddAsync(ProcessData processData, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ProcessData>> GetByMachineIdAsync(
        Guid machineId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(
        Guid machineId,
        CancellationToken cancellationToken = default);
}
