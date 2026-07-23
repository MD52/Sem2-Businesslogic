namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class MachineCycle
{
    /// <summary>
    /// Eindeutige Identifikation eines Maschinenzyklus.
    /// </summary>
    public int CycleId { get; set; }

    /// <summary>
    /// Bezeichnung des Maschinenzyklus.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Betriebszustand der Maschine während des Zyklus.
    /// </summary>
    public MotionStates MotionState { get; set; }

    /// <summary>
    /// Zeitpunkt des Zyklusstarts.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Zeitpunkt des Zyklusendes.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gesamtdauer des Maschinenzyklus.
    /// </summary>
    public double Duration { get; set; }
}
