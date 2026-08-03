namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class MachineStatusDto
{
    public MotionState MotionState { get; init; }
    public int CompletedCycles { get; init; }
    public ParameterDataDto? CurrentParameters { get; init; }
}
