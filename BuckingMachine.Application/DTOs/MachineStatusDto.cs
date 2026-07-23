namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class MachineStatusDto
{
    public MotionStates MotionState { get; init; }
}
