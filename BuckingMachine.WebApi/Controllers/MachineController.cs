namespace BuckingMachine.WebApi.Controllers;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.MachineControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/machine")]
public sealed class MachineController : ControllerBase
{
    private readonly MachineControlUseCase _machineControlUseCase;
    private readonly ReadMachineStatusUseCase _readMachineStatusUseCase;

    public MachineController(MachineControlUseCase machineControlUseCase, ReadMachineStatusUseCase readMachineStatusUseCase)
    {
        _machineControlUseCase = machineControlUseCase;
        _readMachineStatusUseCase = readMachineStatusUseCase;
    }

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
        await _machineControlUseCase.StopAsync();
        return NoContent();
    }

    [HttpPost("reset")]
    public async Task<IActionResult> ResetAsync(CancellationToken cancellationToken)
    {
        await _machineControlUseCase.ResetAsync();
        return NoContent();
    }

    [HttpPost("fault")]
    public async Task<IActionResult> TriggerFaultAsync()
    {
        await _machineControlUseCase.TriggerFaultAsync();
        return NoContent();
    }

    [HttpGet("status")]
    public async Task<ActionResult<MachineStatusDto>> GetStatusAsync(CancellationToken cancellationToken)
    {
        MachineStatusDto status = await _readMachineStatusUseCase.ExecuteAsync();
        return Ok(status);
    }
}
