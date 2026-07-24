namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class MachineCycleDto
{
    public int CycleId { get; init; }
    public string Name { get; init; } = string.Empty;
    public MotionStates MotionState { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public double Duration { get; init; }
    public IReadOnlyCollection<ProcessDataDto> ProcessData { get; init; } =
        Array.Empty<ProcessDataDto>();
}
