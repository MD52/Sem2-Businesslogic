namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.Interfaces;

public sealed class GetCycleHistoryUseCase
{
    private readonly IProcessDataRepository _processDataRepository;

    public GetCycleHistoryUseCase(IProcessDataRepository processDataRepository)
    {
        _processDataRepository = processDataRepository;
    }

    public async Task<IReadOnlyCollection<DTOs.MachineCycleDto>> ExecuteAsync(
        Guid machineId,
        CancellationToken cancellationToken = default)
    {
        var cycles = await LoadCycleHistoryAsync(machineId, cancellationToken);
        return MapToDtos(cycles);
    }

    private Task<IReadOnlyCollection<Domain.Entities.MachineCycle>> LoadCycleHistoryAsync(
        Guid machineId,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.GetCycleHistoryAsync(machineId, cancellationToken);
    }

    private static IReadOnlyCollection<DTOs.MachineCycleDto> MapToDtos(
        IReadOnlyCollection<Domain.Entities.MachineCycle> cycles)
    {
        throw new NotImplementedException();
    }
}
