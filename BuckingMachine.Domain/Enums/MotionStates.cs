namespace BuckingMachine.Domain.Enums;

public enum MotionStates
{
    /// <summary>
    /// Maschine ist bereit, aber nicht in Betrieb.
    /// </summary>
    Idle,

    /// <summary>
    /// Maschine befindet sich im Betrieb.
    /// </summary>
    Active,

    /// <summary>
    /// Maschine befindet sich im Störungszustand.
    /// </summary>
    Faulted
}
