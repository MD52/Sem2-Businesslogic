namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class Operator
{
    /// <summary>
    /// Eindeutige Identifikation des Bedieners.
    /// </summary>
    public int OperatorId { get; set; }

    /// <summary>
    /// Benutzername für die Anmeldung an der Web-HMI.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Anzeigename des Bedieners in der Benutzeroberfläche.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Rolle des Benutzers innerhalb des Systems.
    /// </summary>
    public OperatorRole Role { get; set; }

    /// <summary>
    /// Gibt an, ob der Bediener aktuell angemeldet ist.
    /// </summary>
    public bool IsLoggedIn { get; set; }
}
