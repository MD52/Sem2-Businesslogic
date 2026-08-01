namespace BuckingMachine.Domain.Entities;

public sealed class BuckingMachine
{
    public int MachineId { get; init; }
    public string Name { get; init; } = string.Empty;
    public ParameterData ParameterData { get; init; } = null!;
    public StatusData StatusData { get; init; } = null!;
    public Alarm? ActiveAlarm { get; init; }
}
