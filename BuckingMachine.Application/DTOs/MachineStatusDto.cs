namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class MachineStatusDto
{
    public MotionState MotionState { get; init; }
}
