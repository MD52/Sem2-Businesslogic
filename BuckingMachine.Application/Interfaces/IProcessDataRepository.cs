namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Entities;

public interface IProcessDataRepository
{
    Task<int> SaveParameterDataAsync(ParameterData parameterData, CancellationToken cancellationToken = default);
}
