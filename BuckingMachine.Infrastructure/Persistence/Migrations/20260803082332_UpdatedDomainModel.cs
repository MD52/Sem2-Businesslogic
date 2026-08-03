using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuckingMachine.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDomainModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    AlarmId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Severity = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.AlarmId);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    OperatorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLoggedIn = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.OperatorId);
                });

            migrationBuilder.CreateTable(
                name: "ParameterData",
                columns: table => new
                {
                    ParameterDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperationModeSideDrives = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetVelocitySideDrives = table.Column<double>(type: "REAL", nullable: false),
                    TargetTorqueSideDrives = table.Column<double>(type: "REAL", nullable: false),
                    TargetPosSideDrives = table.Column<double>(type: "REAL", nullable: false),
                    OperationModeMainDrives = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetVelocityMainDrives = table.Column<double>(type: "REAL", nullable: false),
                    TargetTorqueMainDrives = table.Column<double>(type: "REAL", nullable: false),
                    TargetPosMainDrives = table.Column<double>(type: "REAL", nullable: false),
                    BreakTimeHoldPos = table.Column<double>(type: "REAL", nullable: false),
                    ReleaseTimeHoldPos = table.Column<double>(type: "REAL", nullable: false),
                    AmountCycleMovements = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterData", x => x.ParameterDataId);
                    table.ForeignKey(
                        name: "FK_ParameterData_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusData",
                columns: table => new
                {
                    StatusDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MotionState = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationModeSideDrives = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualVelocitySideDrives = table.Column<double>(type: "REAL", nullable: false),
                    ActualTorqueSideDrives = table.Column<double>(type: "REAL", nullable: false),
                    ActualPosSideDrives = table.Column<double>(type: "REAL", nullable: false),
                    OperationModeMainDrives = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualVelocityMainDrives = table.Column<double>(type: "REAL", nullable: false),
                    ActualTorqueMainDrives = table.Column<double>(type: "REAL", nullable: false),
                    ActualPosMainDrives = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusData", x => x.StatusDataId);
                    table.ForeignKey(
                        name: "FK_StatusData_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineCycles",
                columns: table => new
                {
                    CycleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParameterDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Duration = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineCycles", x => x.CycleId);
                    table.ForeignKey(
                        name: "FK_MachineCycles_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachineCycles_ParameterData_ParameterDataId",
                        column: x => x.ParameterDataId,
                        principalTable: "ParameterData",
                        principalColumn: "ParameterDataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachineCycles_StatusData_StatusDataId",
                        column: x => x.StatusDataId,
                        principalTable: "StatusData",
                        principalColumn: "StatusDataId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineCycles_MachineId",
                table: "MachineCycles",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineCycles_ParameterDataId",
                table: "MachineCycles",
                column: "ParameterDataId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineCycles_StatusDataId",
                table: "MachineCycles",
                column: "StatusDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterData_MachineId",
                table: "ParameterData",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusData_MachineId",
                table: "StatusData",
                column: "MachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "MachineCycles");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "ParameterData");

            migrationBuilder.DropTable(
                name: "StatusData");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
