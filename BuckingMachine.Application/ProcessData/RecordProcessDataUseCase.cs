namespace BuckingMachine.Application.ProcessData;

using BuckingMachine.Application.Interfaces;

public sealed class RecordProcessDataUseCase
{
    private readonly IProcessDataRepository _processDataRepository;

    public RecordProcessDataUseCase(IProcessDataRepository processDataRepository)
    {
        _processDataRepository = processDataRepository;
    }

    public async Task ExecuteAsync(
        DTOs.ProcessDataDto processData,
        CancellationToken cancellationToken = default)
    {
        ValidateData(processData);
        var entity = MapToEntity(processData);
        await SaveProcessDataAsync(entity, cancellationToken);
    }

    private static void ValidateData(DTOs.ProcessDataDto processData)
    {
        throw new NotImplementedException();
    }

    private static Domain.Entities.ProcessData MapToEntity(DTOs.ProcessDataDto processData)
    {
        throw new NotImplementedException();
    }

    private Task SaveProcessDataAsync(
        Domain.Entities.ProcessData processData,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.AddAsync(processData, cancellationToken);
    }
}
