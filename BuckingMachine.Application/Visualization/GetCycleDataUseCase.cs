namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class GetCycleHistoryUseCase
{
    private readonly IProcessDataRepository _processDataRepository;

    public GetCycleHistoryUseCase(
        IProcessDataRepository processDataRepository)
    {
        _processDataRepository = processDataRepository;
    }

    public async Task<IReadOnlyCollection<MachineCycleDto>> ExecuteAsync(
        Guid machineId,
        DateTime? from,
        DateTime? to,
        int? limit,
        CancellationToken cancellationToken = default)
    {
        ValidateFilter(from, to, limit);

        IReadOnlyCollection<MachineCycle> cycles =
            await LoadCycleHistoryAsync(
                machineId,
                from,
                to,
                limit,
                cancellationToken);

        return MapToDtos(cycles);
    }

    private static void ValidateFilter(
        DateTime? from,
        DateTime? to,
        int? limit)
    {
        if (from.HasValue &&
            to.HasValue &&
            from.Value > to.Value)
        {
            throw new ArgumentException(
                "Das Startdatum darf nicht nach dem Enddatum liegen.");
        }

        if (limit.HasValue && limit.Value <= 0)
        {
            throw new ArgumentException(
                "Das Limit muss grösser als 0 sein.");
        }
    }

    private Task<IReadOnlyCollection<MachineCycle>>
        LoadCycleHistoryAsync(
            Guid machineId,
            DateTime? from,
            DateTime? to,
            int? limit,
            CancellationToken cancellationToken)
    {
        return _processDataRepository.GetCycleHistoryAsync(
            machineId,
            from,
            to,
            limit,
            cancellationToken);
    }

    private static IReadOnlyCollection<MachineCycleDto> MapToDtos(
        IReadOnlyCollection<MachineCycle> cycles)
    {
        return cycles
            .Select(cycle => new MachineCycleDto
            {
                CycleId = cycle.CycleId,
                Name = cycle.Name,
                MotionState = cycle.MotionState,
                StartTime = cycle.StartTime,
                EndTime = cycle.EndTime,
                Duration = cycle.Duration
            })
            .ToList();
    }
}
