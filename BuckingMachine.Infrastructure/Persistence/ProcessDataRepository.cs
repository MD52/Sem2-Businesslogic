namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class ProcessDataRepository(BuckingMachineDbContext dbContext) : IProcessDataRepository
{
    public async Task<int> SaveParameterDataAsync(ParameterData data, CancellationToken token = default)
    {
        dbContext.ParameterData.Add(data);
        await dbContext.SaveChangesAsync(token);
        return data.ParameterDataId;
    }

    public async Task<int> SaveStatusDataAsync(StatusData data, CancellationToken token = default)
    {
        dbContext.StatusData.Add(data);
        await dbContext.SaveChangesAsync(token);
        return data.StatusDataId;
    }

    public async Task<int> SaveMachineCycleAsync(MachineCycle cycle, CancellationToken token = default)
    {
        await EnsureReferencesExistAsync(cycle, token);
        dbContext.MachineCycles.Add(cycle);
        await dbContext.SaveChangesAsync(token);
        return cycle.CycleId;
    }

    public async Task<int> SaveCompletedCycleAsync(StatusData status, MachineCycle cycle, CancellationToken token = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(token);
        dbContext.StatusData.Add(status);
        await dbContext.SaveChangesAsync(token);

        var storedCycle = new MachineCycle
        {
            MachineId=cycle.MachineId, ParameterDataId=cycle.ParameterDataId, StatusDataId=status.StatusDataId,
            Name=cycle.Name, StartTime=cycle.StartTime, EndTime=cycle.EndTime, Duration=cycle.Duration
        };
        await EnsureReferencesExistAsync(storedCycle, token);
        dbContext.MachineCycles.Add(storedCycle);
        await dbContext.SaveChangesAsync(token);
        await transaction.CommitAsync(token);
        return storedCycle.CycleId;
    }

    public Task<MachineCycle?> GetMachineCycleAsync(int id, CancellationToken token = default) =>
        dbContext.MachineCycles.AsNoTracking().SingleOrDefaultAsync(x => x.CycleId == id, token);
    public Task<ParameterData?> GetParameterDataAsync(int id, CancellationToken token = default) =>
        dbContext.ParameterData.AsNoTracking().SingleOrDefaultAsync(x => x.ParameterDataId == id, token);
    public Task<StatusData?> GetStatusDataAsync(int id, CancellationToken token = default) =>
        dbContext.StatusData.AsNoTracking().SingleOrDefaultAsync(x => x.StatusDataId == id, token);

    public async Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(DateTime? from, DateTime? to, int? limit, CancellationToken token = default)
    {
        IQueryable<MachineCycle> query = dbContext.MachineCycles.AsNoTracking();
        if (from.HasValue) query = query.Where(x => x.StartTime >= from.Value);
        if (to.HasValue) query = query.Where(x => x.StartTime <= to.Value);
        query = query.OrderByDescending(x => x.StartTime);
        if (limit.HasValue) query = query.Take(limit.Value);
        return await query.ToArrayAsync(token);
    }

    private async Task EnsureReferencesExistAsync(MachineCycle cycle, CancellationToken token)
    {
        if (!await dbContext.ParameterData.AnyAsync(x => x.ParameterDataId == cycle.ParameterDataId, token))
            throw new InvalidOperationException("Der referenzierte Parametersatz ist nicht gespeichert.");
        if (!await dbContext.StatusData.AnyAsync(x => x.StatusDataId == cycle.StatusDataId, token))
            throw new InvalidOperationException("Der referenzierte Statusdatensatz ist nicht gespeichert.");
    }
}
