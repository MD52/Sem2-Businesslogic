namespace BuckingMachine.Infrastructure.Simulation;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Application.MachineControl;
using BuckingMachine.Domain.Entities;
using BuckingMachine.Domain.Enums;

/// <summary>
/// Einfache Simulation der Bucking-Maschine.
///
/// Die Klasse simuliert:
/// - einen einzelnen Zyklus,
/// - einen kontinuierlichen Betrieb,
/// - Stop und Reset,
/// - einen manuell auslösbaren Fault-Zustand,
/// - die Übernahme der zuletzt eingestellten Parameter,
/// - das Lesen des aktuellen Zustands.
///
/// Die Klasse enthält absichtlich keine umfangreichen Startprüfungen,
/// damit der Ablauf für die Semesterarbeit möglichst kompakt bleibt.
/// </summary>
public sealed class MqttMachineSimulator : IMachineControlGateway, IAsyncDisposable
{
    // Feste Zeiten für die Simulation.
    private static readonly TimeSpan CycleDuration =
        TimeSpan.FromSeconds(5);

    private static readonly TimeSpan PauseBetweenCycles =
        TimeSpan.FromSeconds(2);

    // Aktueller Zustand der simulierten Maschine.
    private MotionState _motionState = MotionState.Idle;

    // Lokaler Zähler für vollständig abgeschlossene Zyklen.
    private int _completedCycles;

    // Zuletzt vom Frontend übernommener Parametersatz.
    // Dieser Parametersatz wird beim nächsten Zyklus verwendet.
    private ParameterData? _currentParameters;

    // Wird benötigt, um einen kontinuierlichen Betrieb mit StopAsync()
    // abbrechen zu können.
    private CancellationTokenSource? _runCancellation;

    // Referenz auf die aktuell laufende Sequenz.
    private Task? _runningSequence;

    /// <summary>
    /// Startet genau einen Zyklus.
    ///
    /// Hier wird RunSequenceAsync() direkt aufgerufen.
    /// false bedeutet: Die Sequenz wird nur einmal ausgeführt.
    /// </summary>
    /// 
    public Task StartSingleCycleAsync(
        CancellationToken cancellationToken = default)
    {
        _runCancellation?.Dispose();

        _runCancellation =
            CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken);

        _runningSequence = RunSequenceAsync(
            runContinuously: false,
            _runCancellation.Token);

        // Der Startbefehl wird sofort bestätigt.
        // Die Sequenz läuft währenddessen asynchron weiter.
        return Task.CompletedTask;
    }

    /// <summary>
    /// Startet den kontinuierlichen Betrieb.
    ///
    /// true bedeutet: Nach jedem Zyklus und der festen Pause
    /// wird erneut ein Zyklus ausgeführt, bis StopAsync() aufgerufen wird.
    /// </summary>
    public Task StartContinuousAsync(
        CancellationToken cancellationToken = default)
    {
        _runCancellation?.Dispose();

        _runCancellation =
            CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken);

        _runningSequence = RunSequenceAsync(
            runContinuously: true,
            _runCancellation.Token);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stoppt die laufende Sequenz und setzt den Zustand auf Idle.
    /// </summary>
    public async Task StopAsync()
    {
        if (_runCancellation is not null)
        {
            await _runCancellation.CancelAsync();
        }

        if (_runningSequence is not null)
        {
            try
            {
                await _runningSequence;
            }
            catch (OperationCanceledException)
            {
                // Der Abbruch durch StopAsync() ist erwartet.
            }
        }

        _motionState = MotionState.Idle;
    }

    /// <summary>
    /// Stoppt die Simulation, setzt den Zykluszähler auf null
    /// und setzt den Zustand auf Idle.
    ///
    /// Die zuletzt übernommenen Parameter bleiben gespeichert.
    /// </summary>
    public async Task ResetAsync()
    {
        await StopAsync();

        _completedCycles = 0;
        _motionState = MotionState.Idle;
    }

    /// <summary>
    /// Löst für Test- und Präsentationszwecke einen Fault-Zustand aus.
    /// Eine laufende Sequenz wird vorher gestoppt.
    /// </summary>
    public async Task TriggerFaultAsync()
    {
        await StopAsync();

        _motionState = MotionState.Faulted;
    }

    /// <summary>
    /// Übernimmt den vollständigen Parametersatz aus dem Frontend.
    ///
    /// Die persistente Speicherung in MySQL erfolgt nicht hier,
    /// sondern über den RecordProcessDataUseCase.
    /// </summary>
    public Task UpdateParametersAsync(
        ParameterData parameterData)
    {
        ArgumentNullException.ThrowIfNull(parameterData);

        _currentParameters = parameterData;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gibt den aktuellen Zustand, den Zykluszähler und die zuletzt
    /// übernommenen Parameter zurück.
    /// </summary>
    public Task<MachineSimulationStatus> ReadStateAsync()
    {
        var status = new MachineSimulationStatus(
            MotionState: _motionState,
            CompletedCycles: _completedCycles,
            CurrentParameters: _currentParameters);

        return Task.FromResult(status);
    }




    /// <summary>
    /// Führt entweder einen einzelnen Zyklus oder mehrere Zyklen aus.
    ///
    /// runContinuously = false:
    ///     genau ein Zyklus
    ///
    /// runContinuously = true:
    ///     Zyklen wiederholen, bis der CancellationToken abgebrochen wird
    /// </summary>
    private async Task RunSequenceAsync(
        bool runContinuously,
        CancellationToken cancellationToken)
    {
        try
        {
            do
            {
                await ExecuteCycleAsync(cancellationToken);

                // Die Pause ist nur für den kontinuierlichen Betrieb nötig.
                if (runContinuously)
                {
                    await Task.Delay(
                        PauseBetweenCycles,
                        cancellationToken);
                }
            }
            while (runContinuously);

            _motionState = MotionState.Idle;
        }
        catch (OperationCanceledException)
        {
            // StopAsync() bricht Task.Delay() über den CancellationToken ab.
            _motionState = MotionState.Idle;
        }
        catch
        {
            // Unerwartete Fehler führen zum Fault-Zustand.
            _motionState = MotionState.Faulted;
            throw;
        }
    }

    /// <summary>
    /// Simuliert genau einen Maschinenzyklus.
    /// </summary>
    private async Task ExecuteCycleAsync(
        CancellationToken cancellationToken)
    {
        // Für den simulierten Zyklus wird PositionControl verwendet.
        DriveOperationMode cycleMode =
            DriveOperationMode.PositionControl;

        // cycleMode wird in dieser einfachen Simulation nur dokumentiert.
        _ = cycleMode;

        // Start des Zyklus.
        _motionState = MotionState.Active;

        // Simulierte Bearbeitungszeit.
        await Task.Delay(
            CycleDuration,
            cancellationToken);

        // Der Zyklus wurde vollständig abgeschlossen.
        _completedCycles++;

        // Nach einem Zyklus befindet sich die Maschine wieder in Idle.
        _motionState = MotionState.Idle;
    }

    /// <summary>
    /// Gibt beim Beenden der Anwendung verwendete Ressourcen frei.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_runCancellation is not null)
        {
            await _runCancellation.CancelAsync();
            _runCancellation.Dispose();
        }

        if (_runningSequence is not null)
        {
            try
            {
                await _runningSequence;
            }
            catch (OperationCanceledException)
            {
                // Erwarteter Abbruch beim Beenden der Anwendung.
            }
        }
    }
}
