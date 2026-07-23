namespace BuckingMachine.WebApi.Controllers;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.MachineControl;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/machine")]

public class MachineController : ControllerBase
{
    private readonly StartMachineUseCase _startMachineUseCase;
    private readonly StopMachineUseCase _stopMachineUseCase;
    private readonly ReadMachineStatusUseCase _readMachineStatusUseCase;

    public MachineController(
        StartMachineUseCase startMachineUseCase,
        StopMachineUseCase stopMachineUseCase,
        ReadMachineStatusUseCase readMachineStatusUseCase)
    {
        _startMachineUseCase = startMachineUseCase;
        _stopMachineUseCase = stopMachineUseCase;
        _readMachineStatusUseCase = readMachineStatusUseCase;
    }


    [HttpPost("start")]
    public async Task<IActionResult> StartAsync()
    {
        await _startMachineUseCase.ExecuteAsync();

        return Ok();
    }


    [HttpPost("stop")]
    public async Task<IActionResult> StopAsync()
    {
        await _stopMachineUseCase.ExecuteAsync();

        return Ok();
    }

    [HttpGet("status")]
    public async Task<ActionResult<MachineStatusDto>> GetStatusAsync()
    {
        MachineStatusDto status =
            await _readMachineStatusUseCase.ExecuteAsync();

        return Ok(status);
    }
}