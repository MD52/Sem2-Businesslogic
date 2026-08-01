namespace BuckingMachine.Application.Interfaces;

using BuckingMachine.Domain.Entities;

public interface IProcessDataRepository
{
    Task<int> SaveParameterDataAsync(ParameterData parameterData, CancellationToken cancellationToken = default);
    Task<int> SaveStatusDataAsync(StatusData statusData, CancellationToken cancellationToken = default);
    Task<int> SaveMachineCycleAsync(MachineCycle machineCycle, CancellationToken cancellationToken = default);
    Task<int> SaveCompletedCycleAsync(StatusData statusData, MachineCycle machineCycle, CancellationToken cancellationToken = default);
    Task<MachineCycle?> GetMachineCycleAsync(int cycleId, CancellationToken cancellationToken = default);
    Task<ParameterData?> GetParameterDataAsync(int parameterDataId, CancellationToken cancellationToken = default);
    Task<StatusData?> GetStatusDataAsync(int statusDataId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MachineCycle>> GetCycleHistoryAsync(DateTime? from, DateTime? to, int? limit, CancellationToken cancellationToken = default);
}
