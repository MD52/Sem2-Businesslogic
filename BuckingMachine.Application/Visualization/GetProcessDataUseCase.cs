namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.Interfaces;

public sealed class GetProcessDataUseCase
{
    private readonly IProcessDataRepository _processDataRepository;

    public GetProcessDataUseCase(IProcessDataRepository processDataRepository)
    {
        _processDataRepository = processDataRepository;
    }

    public async Task<IReadOnlyCollection<DTOs.ProcessDataDto>> ExecuteAsync(
        Guid machineId,
        CancellationToken cancellationToken = default)
    {
        var processData = await LoadProcessDataAsync(machineId, cancellationToken);
        return MapToDtos(processData);
    }

    private Task<IReadOnlyCollection<Domain.Entities.ProcessData>> LoadProcessDataAsync(
        Guid machineId,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.GetByMachineIdAsync(machineId, cancellationToken);
    }

    private static IReadOnlyCollection<DTOs.ProcessDataDto> MapToDtos(
        IReadOnlyCollection<Domain.Entities.ProcessData> processData)
    {
        throw new NotImplementedException();
    }
}
