namespace BuckingMachine.Application.DTOs;

public sealed class MachineCycleDto
{
    public int CycleId { get; init; }
    public int MachineId { get; init; }
    public int ParameterDataId { get; init; }
    public int StatusDataId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public double Duration { get; init; }
    public ParameterDataDto? ParameterData { get; init; }
    public StatusDataDto? StatusData { get; init; }
}
