namespace BuckingMachine.Application.DTOs;

using BuckingMachine.Domain.Entities;

internal static class DtoMapper
{
    internal static ParameterDataDto Map(ParameterData p) => new()
    {
        ParameterDataId=p.ParameterDataId, MachineId=p.MachineId, RecordedAt=p.RecordedAt,
        OperationModeSideDrives=p.OperationModeSideDrives, TargetVelocitySideDrives=p.TargetVelocitySideDrives,
        TargetTorqueSideDrives=p.TargetTorqueSideDrives, TargetPosSideDrives=p.TargetPosSideDrives,
        OperationModeMainDrives=p.OperationModeMainDrives, TargetVelocityMainDrives=p.TargetVelocityMainDrives,
        TargetTorqueMainDrives=p.TargetTorqueMainDrives, TargetPosMainDrives=p.TargetPosMainDrives,
        BreakTimeHoldPos=p.BreakTimeHoldPos, ReleaseTimeHoldPos=p.ReleaseTimeHoldPos, AmountCycleMovements=p.AmountCycleMovements
    };
    internal static StatusDataDto Map(StatusData s) => new()
    {
        StatusDataId=s.StatusDataId, MachineId=s.MachineId, Timestamp=s.Timestamp, MotionState=s.MotionState,
        OperationModeSideDrives=s.OperationModeSideDrives, ActualVelocitySideDrives=s.ActualVelocitySideDrives,
        ActualTorqueSideDrives=s.ActualTorqueSideDrives, ActualPosSideDrives=s.ActualPosSideDrives,
        OperationModeMainDrives=s.OperationModeMainDrives, ActualVelocityMainDrives=s.ActualVelocityMainDrives,
        ActualTorqueMainDrives=s.ActualTorqueMainDrives, ActualPosMainDrives=s.ActualPosMainDrives
    };
}
