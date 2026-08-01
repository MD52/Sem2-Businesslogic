namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Enums;

public sealed class ParameterDataDto
{
    public int ParameterDataId { get; init; }
    public int MachineId { get; init; }
    public DateTime RecordedAt { get; init; }
    public DriveOperationMode OperationModeSideDrives { get; init; }
    public double TargetVelocitySideDrives { get; init; }
    public double TargetTorqueSideDrives { get; init; }
    public double TargetPosSideDrives { get; init; }
    public DriveOperationMode OperationModeMainDrives { get; init; }
    public double TargetVelocityMainDrives { get; init; }
    public double TargetTorqueMainDrives { get; init; }
    public double TargetPosMainDrives { get; init; }
    public double BreakTimeHoldPos { get; init; }
    public double ReleaseTimeHoldPos { get; init; }
    public int AmountCycleMovements { get; init; }
}
