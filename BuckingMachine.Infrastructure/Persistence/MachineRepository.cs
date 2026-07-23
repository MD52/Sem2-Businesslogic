namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;



public class MachineRepository : IProcessDataRepository
{
    public Task AddAsync(
        ProcessData processData,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ProcessData>> GetByMachineIdAsync(
        Guid machineId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(
        Guid machineId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BuckingMachine> GetAsync(
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
