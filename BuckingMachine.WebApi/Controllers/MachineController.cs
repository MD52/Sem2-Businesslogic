namespace BuckingMachine.WebApi.Controllers;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.MachineControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/machine")]
public sealed class MachineController : ControllerBase
{
    private readonly MachineControlUseCase _machineControlUseCase;

    public MachineController(MachineControlUseCase machineControlUseCase) =>
        _machineControlUseCase = machineControlUseCase;

    [HttpPost("start-cycle")]
    public async Task<IActionResult> StartCycleAsync(CancellationToken cancellationToken)
    {
        await _machineControlUseCase.StartCycleAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("start-continuous")]
    public async Task<IActionResult> StartContinuousAsync(CancellationToken cancellationToken)
    {
        await _machineControlUseCase.StartContinuousAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("stop")]
    public async Task<IActionResult> StopAsync(CancellationToken cancellationToken)
    {
        await _machineControlUseCase.StopAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("reset")]
    public async Task<IActionResult> ResetAsync(CancellationToken cancellationToken)
    {
        await _machineControlUseCase.ResetAsync(cancellationToken);
        return NoContent();
    }

    [HttpGet("status")]
    public async Task<ActionResult<MachineStatusDto>> GetStatusAsync(CancellationToken cancellationToken)
    {
        MachineStatusDto status = await _machineControlUseCase.ReadStateAsync(cancellationToken);
        return Ok(status);
    }
}
