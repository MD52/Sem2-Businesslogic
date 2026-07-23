namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;




public sealed class ProcessData
{
    /// <summary>
    /// Eindeutige Identifikation des Datensatzes.
    /// </summary>
    public int ProcessDataId { get; set; }

    /// <summary>
    /// Referenz auf den zugehörigen Maschinenzyklus.
    /// </summary>
    public int CycleId { get; set; }

    /// <summary>
    /// Zeitpunkt der Datenerfassung.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gemessene Geschwindigkeit der Seitenantriebe.
    /// </summary>
    public double VelocitySideDrives { get; set; }

    /// <summary>
    /// Gemessenes Drehmoment der Seitenantriebe.
    /// </summary>
    public double TorqueSideDrives { get; set; }

    /// <summary>
    /// Tatsächlich erreichte Position der Seitenantriebe.
    /// </summary>
    public double ActualPosSideDrives { get; set; }

    /// <summary>
    /// Gemessene Geschwindigkeit des Hauptantriebs.
    /// </summary>
    public double VelocityMainDrives { get; set; }

    /// <summary>
    /// Gemessenes Drehmoment des Hauptantriebs.
    /// </summary>
    public double TorqueMainDrives { get; set; }

    /// <summary>
    /// Tatsächlich erreichte Position des Hauptantriebs.
    /// </summary>
    public double ActualPosMainDrives { get; set; }

    /// <summary>
    /// Maschinenzustand zum Zeitpunkt der Datenerfassung.
    /// </summary>
    public MotionStates MotionState { get; set; }
}
