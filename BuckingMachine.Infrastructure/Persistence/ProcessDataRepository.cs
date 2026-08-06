namespace BuckingMachine.Infrastructure.Persistence;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class ProcessDataRepository : IProcessDataRepository
{
    private readonly BuckingMachineDbContext _dbContext;

    public ProcessDataRepository(BuckingMachineDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveParameterDataAsync(
        ParameterData parameterData,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameterData);

        await _dbContext.ParameterData.AddAsync(
            parameterData,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return parameterData.ParameterDataId;
    }
}
