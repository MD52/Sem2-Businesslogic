namespace BuckingMachine.WebApi.Controllers;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.ProcessData;
using BuckingMachine.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/process-data")]
public sealed class ProcessDataController : ControllerBase
{
    private readonly RecordProcessDataUseCase _recordProcessDataUseCase;

    public ProcessDataController(RecordProcessDataUseCase recordProcessDataUseCase) =>
        _recordProcessDataUseCase = recordProcessDataUseCase;

    [HttpPut("parameters")]
    public async Task<IActionResult> UpdateParametersAsync(
        ParameterDataDto parameterData,
        CancellationToken cancellationToken)
    {
        var domainParameterData = new ParameterData
        {
            ParameterDataId = parameterData.ParameterDataId,
            MachineId = parameterData.MachineId,
            RecordedAt = parameterData.RecordedAt,
            OperationModeSideDrives = parameterData.OperationModeSideDrives,
            TargetVelocitySideDrives = parameterData.TargetVelocitySideDrives,
            TargetTorqueSideDrives = parameterData.TargetTorqueSideDrives,
            TargetPosSideDrives = parameterData.TargetPosSideDrives,
            OperationModeMainDrives = parameterData.OperationModeMainDrives,
            TargetVelocityMainDrives = parameterData.TargetVelocityMainDrives,
            TargetTorqueMainDrives = parameterData.TargetTorqueMainDrives,
            TargetPosMainDrives = parameterData.TargetPosMainDrives,
            BreakTimeHoldPos = parameterData.BreakTimeHoldPos,
            ReleaseTimeHoldPos = parameterData.ReleaseTimeHoldPos,
            AmountCycleMovements = parameterData.AmountCycleMovements
        };

        await _recordProcessDataUseCase.SaveParameterDataAsync(domainParameterData, cancellationToken);
        return NoContent();
    }
}
