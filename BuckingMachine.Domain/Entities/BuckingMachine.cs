namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class BuckingMachine
{
    /// <summary>
    /// Eindeutige Bezeichnung der Bucking-Maschine.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Aktueller Betriebszustand der Maschine (Idle, Active oder Faulted).
    /// </summary>
    public MotionStates MotionState { get; set; }

    /// <summary>
    /// Gewählter Betriebsmodus der Seitenantriebe.
    /// </summary>
    public DriveOperationMode OperationModeSideDrives { get; set; }

    /// <summary>
    /// Sollgeschwindigkeit der Seitenantriebe.
    /// </summary>
    public double VelocitySideDrives { get; set; }

    /// <summary>
    /// Soll-Drehmoment der Seitenantriebe.
    /// </summary>
    public double TorqueSideDrives { get; set; }

    /// <summary>
    /// Zielposition der Seitenantriebe.
    /// </summary>
    public double TargetPosSideDrives { get; set; }

    /// <summary>
    /// Gewählter Betriebsmodus des Hauptantriebs.
    /// </summary>
    public DriveOperationMode OperationModeMainDrives { get; set; }

    /// <summary>
    /// Sollgeschwindigkeit des Hauptantriebs.
    /// </summary>
    public double VelocityMainDrives { get; set; }

    /// <summary>
    /// Soll-Drehmoment des Hauptantriebs.
    /// </summary>
    public double TorqueMainDrives { get; set; }

    /// <summary>
    /// Zielposition des Hauptantriebs.
    /// </summary>
    public double TargetPosMainDrives { get; set; }

    /// <summary>
    /// Haltezeit vor dem Öffnen bzw. Lösen der Klammern.
    /// </summary>
    public double BreakTimeHoldPos { get; set; }

    /// <summary>
    /// Zeitdauer zum Freigeben der Halteposition.
    /// </summary>
    public double ReleaseTimeHoldPos { get; set; }

    /// <summary>
    /// Anzahl Bewegungen, die innerhalb eines Maschinenzyklus ausgeführt werden.
    /// </summary>
    public int AmountCycleMovements { get; set; }

    /// <summary>
    /// Zeigt an, ob mindestens ein aktiver Alarm vorhanden ist.
    /// </summary>
    public bool AlarmSumm { get; set; }
}
