namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class GetCycleHistoryUseCase
{
    private readonly IProcessDataRepository _repository;
    public GetCycleHistoryUseCase(IProcessDataRepository repository) => _repository = repository;

    public async Task<IReadOnlyCollection<MachineCycleDto>> ExecuteAsync(DateTime? from = null, DateTime? to = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        ValidateFilter(from, to, limit);
        return MapToDtos(await LoadCycleHistoryAsync(from, to, limit, cancellationToken));
    }

    private static void ValidateFilter(DateTime? from, DateTime? to, int? limit)
    {
        if (from > to) throw new ArgumentException("Das Startdatum darf nicht nach dem Enddatum liegen.");
        if (limit <= 0) throw new ArgumentException("Das Limit muss grösser als 0 sein.", nameof(limit));
    }

    private Task<IReadOnlyCollection<MachineCycle>> LoadCycleHistoryAsync(DateTime? from, DateTime? to, int? limit, CancellationToken token) =>
        _repository.GetCycleHistoryAsync(from, to, limit, token);

    private static IReadOnlyCollection<MachineCycleDto> MapToDtos(IEnumerable<MachineCycle> cycles) => cycles.Select(c => new MachineCycleDto
    {
        CycleId = c.CycleId, MachineId = c.MachineId, ParameterDataId = c.ParameterDataId,
        StatusDataId = c.StatusDataId, Name = c.Name, StartTime = c.StartTime, EndTime = c.EndTime, Duration = c.Duration
    }).ToArray();
}
