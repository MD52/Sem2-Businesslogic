# Commit-Log: MySQL-Integration

## 814e1cd – Simplify process data persistence and add database context

Das Interface `IProcessDataRepository` wurde auf die Speicherung von `ParameterData` reduziert. Nicht mehr benötigte Zyklus- und Historienabhängigkeiten wurden entfernt. Außerdem wurde der `BuckingMachineDbContext` als Grundlage für Entity Framework Core angelegt.

## 0a241d4 – Use EF Core in process data repository

Das `ProcessDataRepository` wurde von einer temporären In-Memory-Speicherung auf Entity Framework Core umgestellt. Es verwendet nun den `BuckingMachineDbContext`, ruft `SaveChangesAsync()` auf und gibt die erzeugte `ParameterDataId` zurück.

## aa59cc8 – Align Entity Framework and Pomelo package versions

Die Entity-Framework-Pakete wurden auf Version `9.0.18` gesetzt und damit an `Pomelo.EntityFrameworkCore.MySql 9.0.0` angepasst. Dadurch werden die vorherigen Paketkonflikte und Build-Warnungen vermieden.

## committed – Configure MySQL database context

In `Program.cs` wurden der Connection String, `AddDbContext`, `UseMySql` und MySQL 8 konfiguriert. Das `ProcessDataRepository` wird als `Scoped` registriert. In `appsettings.json` wurde ein Connection String mit dem Passwort-Platzhalter `CHANGE_ME` ergänzt.

Vorgesehene Commit-Nachricht:

```text
Configure MySQL database context
```
