namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class Alarm
{
    public int AlarmId { get; init; }
    public int MachineId { get; init; }
    public int? StatusDataId { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public AlarmSeverity Severity { get; init; }
    public bool IsActive { get; init; }
    public DateTime OccurredAt { get; init; }
    public StatusData? StatusData { get; init; }
}
