namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class Alarm
{
    /// <summary>
    /// Eindeutige Identifikation des Alarms.
    /// </summary>
    public int AlarmId { get; set; }

    /// <summary>
    /// Technischer Alarmcode.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Beschreibung der Störung oder Warnung.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Priorität bzw. Schweregrad des Alarms.
    /// </summary>
    public AlarmSeverity Severity { get; set; }

    /// <summary>
    /// Gibt an, ob der Alarm aktuell aktiv ist.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Zeitpunkt des Auftretens des Alarms.
    /// </summary>
    public DateTime OccurredAt { get; set; }
}
