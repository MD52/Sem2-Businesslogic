namespace BuckingMachine.Domain.Entities;

public sealed class MachineCycle
{
    public int CycleId { get; init; }
    public int MachineId { get; init; }
    public int ParameterDataId { get; init; }
    public int StatusDataId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public double Duration { get; init; }
    public ParameterData ParameterData { get; init; } = null!;
    public StatusData StatusData { get; init; } = null!;
}
