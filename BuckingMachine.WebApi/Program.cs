using BuckingMachine.Application.Authentication;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Application.MachineControl;
using BuckingMachine.Application.ProcessData;
using BuckingMachine.Application.Visualization;
using BuckingMachine.Infrastructure.Authentication;
using BuckingMachine.Infrastructure.Mqtt;
using BuckingMachine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Web API
builder.Services.AddControllers();

// Infrastructure
builder.Services.AddScoped<IMachineCommandGateway, MqttMachineCommandGateway>();
builder.Services.AddScoped<IMachineStatusGateway, MqttMachineStatusGateway>();
builder.Services.AddScoped<IProcessDataRepository, ProcessDataRepository>();
builder.Services.AddDbContext<BuckingMachineDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BuckingMachine") ?? "Data Source=bucking-machine.db"));
builder.Services.AddScoped<IAuthenticationService, JwtTokenService>();

// Authentication
builder.Services.AddScoped<AuthenticateOperatorUseCase>();

// Machine Control
builder.Services.AddScoped<StartMachineUseCase>();
builder.Services.AddScoped<StopMachineUseCase>();
builder.Services.AddScoped<ReadMachineStatusUseCase>();

// Process Data
builder.Services.AddScoped<RecordProcessDataUseCase>();

// Visualization
builder.Services.AddScoped<GetCycleDataUseCase>();
builder.Services.AddScoped<GetCycleHistoryUseCase>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
