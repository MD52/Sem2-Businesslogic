namespace BuckingMachine.Domain.Enums;

public enum AlarmSeverity
{
    /// <summary>
    /// Informative Meldung.
    /// </summary>
    Info,

    /// <summary>
    /// Warnung, die beobachtet werden sollte.
    /// </summary>
    Warning,

    /// <summary>
    /// Kritische Störung mit Einfluss auf den Maschinenbetrieb.
    /// </summary>
    Fault
}
