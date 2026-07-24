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
        if (processData.CycleId <= 0)
        {
            throw new ArgumentException(
                "Die CycleId muss grösser als 0 sein.",
                nameof(processData));
        }

        if (processData.Timestamp == default)
        {
            throw new ArgumentException(
                "Der Erfassungszeitpunkt muss angegeben werden.",
                nameof(processData));
        }
    }

    private static Domain.Entities.ProcessData MapToEntity(DTOs.ProcessDataDto processData)
    {
        return new Domain.Entities.ProcessData
        {
            ProcessDataId = processData.ProcessDataId,
            CycleId = processData.CycleId,
            Timestamp = processData.Timestamp,
            VelocitySideDrives = processData.VelocitySideDrives,
            TorqueSideDrives = processData.TorqueSideDrives,
            ActualPosSideDrives = processData.ActualPosSideDrives,
            VelocityMainDrives = processData.VelocityMainDrives,
            TorqueMainDrives = processData.TorqueMainDrives,
            ActualPosMainDrives = processData.ActualPosMainDrives,
            MotionState = processData.MotionState
        };
    }

    private Task SaveProcessDataAsync(
        Domain.Entities.ProcessData processData,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.SaveProcessDataAsync(
            processData,
            cancellationToken);
    }
}
