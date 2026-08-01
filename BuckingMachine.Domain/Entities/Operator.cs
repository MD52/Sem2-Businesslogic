namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class Operator
{
    /// <summary>
    /// Eindeutige Identifikation des Bedieners.
    /// </summary>
    public int OperatorId { get; init; }

    /// <summary>
    /// Benutzername für die Anmeldung an der Web-HMI.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Anzeigename des Bedieners in der Benutzeroberfläche.
    /// </summary>
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>
    /// Rolle des Benutzers innerhalb des Systems.
    /// </summary>
    public OperatorRole Role { get; init; }

    /// <summary>
    /// Gibt an, ob der Bediener aktuell angemeldet ist.
    /// </summary>
    public bool IsLoggedIn { get; init; }
}
