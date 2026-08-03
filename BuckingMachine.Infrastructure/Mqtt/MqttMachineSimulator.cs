namespace BuckingMachine.Infrastructure.Simulation;

using BuckingMachine.Application.Interfaces;
using BuckingMachine.Application.MachineControl;
using BuckingMachine.Domain.Entities;
using BuckingMachine.Domain.Enums;

public sealed class MqttMachineSimulator : IMachineControlGateway, IAsyncDisposable
{
    private static readonly TimeSpan CycleDuration = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan CyclePause = TimeSpan.FromSeconds(2);

    private readonly SemaphoreSlim _stateLock = new(1, 1);

    private CancellationTokenSource? _runCancellation;
    private Task? _runTask;

    private MotionState _motionState = MotionState.Idle;
    private ParameterData? _currentParameters;
    private int _completedCycles;

    public Task StartSingleCycleAsync(
        CancellationToken cancellationToken = default)
    {
        return StartAsync(false, cancellationToken);
    }

    public Task StartContinuousAsync(
        CancellationToken cancellationToken = default)
    {
        return StartAsync(true, cancellationToken);
    }

    public async Task StopAsync(
        CancellationToken cancellationToken = default)
    {
        CancellationTokenSource? cancellation;
        Task? runningTask;

        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            cancellation = _runCancellation;
            runningTask = _runTask;
            cancellation?.Cancel();
        }
        finally
        {
            _stateLock.Release();
        }

        if (runningTask is not null)
        {
            try
            {
                await runningTask.WaitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
            }
        }

        await SetStateAsync(MotionState.Idle, cancellationToken);
    }

    public async Task ResetAsync(
        CancellationToken cancellationToken = default)
    {
        await StopAsync(cancellationToken);

        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            _completedCycles = 0;
            _motionState = MotionState.Idle;
        }
        finally
        {
            _stateLock.Release();
        }
    }

    public async Task UpdateParametersAsync(
        ParameterData parameterData,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameterData);

        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            _currentParameters = parameterData;
        }
        finally
        {
            _stateLock.Release();
        }
    }

    public async Task<MachineSimulationStatus> ReadStateAsync(
        CancellationToken cancellationToken = default)
    {
        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            return new MachineSimulationStatus(
                _motionState,
                _completedCycles,
                _currentParameters);
        }
        finally
        {
            _stateLock.Release();
        }
    }

    private async Task StartAsync(
        bool runContinuously,
        CancellationToken cancellationToken)
    {
        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            if (_runTask is { IsCompleted: false })
            {
                throw new InvalidOperationException(
                    "Die Simulation läuft bereits.");
            }

            if (_motionState == MotionState.Faulted)
            {
                throw new InvalidOperationException(
                    "Die Maschine muss vor dem Start zurückgesetzt werden.");
            }

            if (_currentParameters is null)
            {
                throw new InvalidOperationException(
                    "Vor dem Start müssen Parameter übernommen werden.");
            }

            _runCancellation?.Dispose();
            _runCancellation = new CancellationTokenSource();

            _runTask = RunSequenceAsync(
                runContinuously,
                _runCancellation.Token);
        }
        finally
        {
            _stateLock.Release();
        }

        await Task.CompletedTask;
    }

    private async Task RunSequenceAsync(
        bool runContinuously,
        CancellationToken cancellationToken)
    {
        try
        {
            do
            {
                await ExecuteCycleAsync(cancellationToken);
                await Task.Delay(CyclePause, cancellationToken);

                if (!runContinuously)
                {
                    break;
                }
            }
            while (!cancellationToken.IsCancellationRequested);

            await SetStateAsync(
                MotionState.Idle,
                CancellationToken.None);
        }
        catch (OperationCanceledException)
        {
            await SetStateAsync(
                MotionState.Idle,
                CancellationToken.None);
        }
        catch
        {
            await SetStateAsync(
                MotionState.Faulted,
                CancellationToken.None);

            throw;
        }
    }

    private async Task ExecuteCycleAsync(
        CancellationToken cancellationToken)
    {
        await SetStateAsync(
            MotionState.Active,
            cancellationToken);

        // Der simulierte Zyklus läuft immer im PositionControl-Modus.
        DriveOperationMode cycleMode =
            DriveOperationMode.PositionControl;

        _ = cycleMode;

        await Task.Delay(
            CycleDuration,
            cancellationToken);

        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            _completedCycles++;
        }
        finally
        {
            _stateLock.Release();
        }

        await SetStateAsync(
            MotionState.Idle,
            cancellationToken);
    }

    private async Task SetStateAsync(
        MotionState state,
        CancellationToken cancellationToken)
    {
        await _stateLock.WaitAsync(cancellationToken);
        try
        {
            _motionState = state;
        }
        finally
        {
            _stateLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_runCancellation is not null)
        {
            await _runCancellation.CancelAsync();
            _runCancellation.Dispose();
        }

        if (_runTask is not null)
        {
            try
            {
                await _runTask;
            }
            catch (OperationCanceledException)
            {
            }
        }

        _stateLock.Dispose();
    }
}
