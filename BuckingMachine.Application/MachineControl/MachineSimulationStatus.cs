namespace BuckingMachine.Application.MachineControl;

using BuckingMachine.Domain.Entities;
using BuckingMachine.Domain.Enums;

public sealed record MachineSimulationStatus(
    MotionState MotionState,
    int CompletedCycles,
    ParameterData? CurrentParameters);
