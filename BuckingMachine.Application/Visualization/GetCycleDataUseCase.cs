namespace BuckingMachine.Application.Visualization;

using BuckingMachine.Application.DTOs;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Domain.Entities;

public sealed class GetCycleDataUseCase
{
    private readonly IProcessDataRepository _repository;
    public GetCycleDataUseCase(IProcessDataRepository repository) => _repository = repository;

    public async Task<MachineCycleDto> ExecuteAsync(int cycleId, CancellationToken cancellationToken = default)
    {
        MachineCycle cycle = await LoadCycleAsync(cycleId, cancellationToken)
            ?? throw new KeyNotFoundException($"Zyklus {cycleId} wurde nicht gefunden.");
        ParameterData parameters = await LoadParameterDataAsync(cycle.ParameterDataId, cancellationToken)
            ?? throw new InvalidOperationException("Der Parametersatz des Zyklus wurde nicht gefunden.");
        StatusData status = await LoadStatusDataAsync(cycle.StatusDataId, cancellationToken)
            ?? throw new InvalidOperationException("Der Statusdatensatz des Zyklus wurde nicht gefunden.");
        return MapToDto(cycle, parameters, status);
    }

    private Task<MachineCycle?> LoadCycleAsync(int id, CancellationToken token) => _repository.GetMachineCycleAsync(id, token);
    private Task<ParameterData?> LoadParameterDataAsync(int id, CancellationToken token) => _repository.GetParameterDataAsync(id, token);
    private Task<StatusData?> LoadStatusDataAsync(int id, CancellationToken token) => _repository.GetStatusDataAsync(id, token);

    private static MachineCycleDto MapToDto(MachineCycle c, ParameterData p, StatusData s) => new()
    {
        CycleId = c.CycleId, MachineId = c.MachineId, ParameterDataId = c.ParameterDataId,
        StatusDataId = c.StatusDataId, Name = c.Name, StartTime = c.StartTime, EndTime = c.EndTime, Duration = c.Duration,
        ParameterData = DtoMapper.Map(p), StatusData = DtoMapper.Map(s)
    };
}
