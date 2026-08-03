using BuckingMachine.Application.Authentication;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Application.MachineControl;
using BuckingMachine.Application.ProcessData;
using BuckingMachine.Application.Visualization;
using BuckingMachine.Infrastructure.Authentication;
using BuckingMachine.Infrastructure.Persistence;
using BuckingMachine.Infrastructure.Simulation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IMachineControlGateway, MqttMachineSimulator>();
builder.Services.AddSingleton<IProcessDataRepository, ProcessDataRepository>();
builder.Services.AddScoped<IAuthenticationService, JwtTokenService>();

builder.Services.AddScoped<AuthenticateOperatorUseCase>();
builder.Services.AddScoped<MachineControlUseCase>();
builder.Services.AddScoped<RecordProcessDataUseCase>();
builder.Services.AddScoped<GetCycleDataUseCase>();
builder.Services.AddScoped<GetCycleHistoryUseCase>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
