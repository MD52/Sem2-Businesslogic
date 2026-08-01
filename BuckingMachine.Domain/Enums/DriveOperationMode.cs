namespace BuckingMachine.Domain.Enums;

public enum DriveOperationMode
{
    /// <summary>
    /// Reine Strom-/Drehmomentregelung ohne Positionsvorgabe.
    /// </summary>
    CurrentControl = 0,

    /// <summary>
    /// Kontinuierliche Drehzahlregelung (Wheel Mode), endlose Rotation.
    /// </summary>
    VelocityControl = 1,

    /// <summary>
    /// Standard-Positionsregelung innerhalb einer Umdrehung (0–360°).
    /// </summary>
    PositionControl = 3,

    /// <summary>
    /// Mehrfachumdrehungen (±256 Umdrehungen) für Linearachsen oder Getriebe.
    /// </summary>
    ExtendedPositionControl = 4,

    /// <summary>
    /// Positionsregelung mit zusätzlicher Drehmomentbegrenzung (Goal Current).
    /// </summary>
    CurrentBasedPositionControl = 5,

    /// <summary>
    /// Direkte PWM-/Spannungsregelung ohne Positions- oder Geschwindigkeitsregelung.
    /// </summary>
    PwmControl = 16
}
