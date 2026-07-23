namespace BuckingMachine.Domain.Enums;


public enum DriveModes
{
    CurrentControl = 0,
    VelocityControl = 1,
    PositionControl = 3,
    ExtendedPositionControl = 4,
    CurrentBasedPositionControl = 5,
    PWMControl = 16
}