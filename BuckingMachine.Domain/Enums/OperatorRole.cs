namespace BuckingMachine.Domain.Enums;

public enum OperatorRole
{
    /// <summary>
    /// Standardbenutzer mit Berechtigung zur Maschinenbedienung.
    /// </summary>
    Operator,

    /// <summary>
    /// Benutzer mit erweiterten Berechtigungen, beispielsweise zur Parametrierung oder Benutzerverwaltung.
    /// </summary>
    Administrator
}
