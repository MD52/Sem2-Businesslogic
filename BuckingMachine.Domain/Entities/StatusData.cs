namespace BuckingMachine.Domain.Entities;

using global::BuckingMachine.Domain.Enums;

public sealed class StatusData
{
    public int StatusDataId { get; init; }
    public int MachineId { get; init; }
    public DateTime Timestamp { get; init; }
    public MotionState MotionState { get; init; }
    public DriveOperationMode OperationModeSideDrives { get; init; }
    public double ActualVelocitySideDrives { get; init; }
    public double ActualTorqueSideDrives { get; init; }
    public double ActualPosSideDrives { get; init; }
    public DriveOperationMode OperationModeMainDrives { get; init; }
    public double ActualVelocityMainDrives { get; init; }
    public double ActualTorqueMainDrives { get; init; }
    public double ActualPosMainDrives { get; init; }
}
