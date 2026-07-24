namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class ProcessDataDto
{
    public int ProcessDataId { get; init; }
    public int CycleId { get; init; }
    public DateTime Timestamp { get; init; }
    public double VelocitySideDrives { get; init; }
    public double TorqueSideDrives { get; init; }
    public double ActualPosSideDrives { get; init; }
    public double VelocityMainDrives { get; init; }
    public double TorqueMainDrives { get; init; }
    public double ActualPosMainDrives { get; init; }
    public MotionStates MotionState { get; init; }
}
