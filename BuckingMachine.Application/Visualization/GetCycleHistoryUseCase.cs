namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class GetCycleDataUseCase
{
    private readonly IProcessDataRepository _processDataRepository;

    public GetCycleDataUseCase(
        IProcessDataRepository processDataRepository)
    {
        _processDataRepository = processDataRepository;
    }

    public async Task<MachineCycleDto> ExecuteAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken = default)
    {
        MachineCycle cycle = await LoadCycleAsync(
            machineId,
            cycleId,
            cancellationToken);

        IReadOnlyCollection<ProcessData> processData =
            await LoadProcessDataAsync(
                machineId,
                cycleId,
                cancellationToken);

        return MapToDto(cycle, processData);
    }

    private Task<MachineCycle> LoadCycleAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.GetCycleDataAsync(
            machineId,
            cycleId,
            cancellationToken);
    }

    private Task<IReadOnlyCollection<ProcessData>> LoadProcessDataAsync(
        Guid machineId,
        int cycleId,
        CancellationToken cancellationToken)
    {
        return _processDataRepository.GetProcessDataAsync(
            machineId,
            cycleId,
            cancellationToken);
    }

    private static MachineCycleDto MapToDto(
        MachineCycle cycle,
        IReadOnlyCollection<ProcessData> processData)
    {
        return new MachineCycleDto
        {
            CycleId = cycle.CycleId,
            Name = cycle.Name,
            MotionState = cycle.MotionState,

            ProcessData = processData
                .Select(data => new ProcessDataDto
                {
                    ProcessDataId = data.ProcessDataId,
                    CycleId = data.CycleId,
                    Timestamp = data.Timestamp,
                    VelocitySideDrives = data.VelocitySideDrives,
                    TorqueSideDrives = data.TorqueSideDrives,
                    ActualPosSideDrives = data.ActualPosSideDrives,
                    VelocityMainDrives = data.VelocityMainDrives,
                    TorqueMainDrives = data.TorqueMainDrives,
                    ActualPosMainDrives = data.ActualPosMainDrives,
                    MotionState = data.MotionState
                })
                .ToList()
        };
    }
}
