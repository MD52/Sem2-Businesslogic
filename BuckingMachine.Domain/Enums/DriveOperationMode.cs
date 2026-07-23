namespace BuckingMachine.Domain.Enums;

public enum DriveOperationMode
{
    /// <summary>
    /// Positionsregelung des Antriebs.
    /// </summary>
    Position,

    /// <summary>
    /// Geschwindigkeitsregelung des Antriebs.
    /// </summary>
    Velocity,

    /// <summary>
    /// Drehmomentregelung des Antriebs.
    /// </summary>
    Torque
}
