using BuckingMachine.Application.Authentication;
using BuckingMachine.Application.Interfaces;
using BuckingMachine.Application.MachineControl;
using BuckingMachine.Application.ProcessData;
using BuckingMachine.Infrastructure.Authentication;
using BuckingMachine.Infrastructure.Persistence;
using BuckingMachine.Infrastructure.Simulation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ======================================================
// DATENBANKKONFIGURATION: BEGINN
// ======================================================


string connectionString = builder.Configuration.GetConnectionString("BuckingMachine")
    ?? throw new InvalidOperationException(
        "Der Connection String 'BuckingMachine' wurde nicht konfiguriert.");

builder.Services.AddDbContext<BuckingMachineDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 0))));


builder.Services.AddScoped<IProcessDataRepository, ProcessDataRepository>();

// ======================================================
// DATENBANKKONFIGURATION: ENDE
// ======================================================


builder.Services.AddScoped<IAuthenticationService, JwtTokenService>();
builder.Services.AddSingleton<IMachineControlGateway, MqttMachineSimulator>();
builder.Services.AddScoped<AuthenticateOperatorUseCase>();
builder.Services.AddScoped<MachineControlUseCase>();
builder.Services.AddScoped<ReadMachineStatusUseCase>();
builder.Services.AddScoped<RecordProcessDataUseCase>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
