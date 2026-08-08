using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuckingMachine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParameterData",
                columns: table => new
                {
                    ParameterDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OperationModeSideDrives = table.Column<int>(type: "int", nullable: false),
                    TargetVelocitySideDrives = table.Column<double>(type: "double", nullable: false),
                    TargetTorqueSideDrives = table.Column<double>(type: "double", nullable: false),
                    TargetPosSideDrives = table.Column<double>(type: "double", nullable: false),
                    OperationModeMainDrives = table.Column<int>(type: "int", nullable: false),
                    TargetVelocityMainDrives = table.Column<double>(type: "double", nullable: false),
                    TargetTorqueMainDrives = table.Column<double>(type: "double", nullable: false),
                    TargetPosMainDrives = table.Column<double>(type: "double", nullable: false),
                    BreakTimeHoldPos = table.Column<double>(type: "double", nullable: false),
                    ReleaseTimeHoldPos = table.Column<double>(type: "double", nullable: false),
                    AmountCycleMovements = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterData", x => x.ParameterDataId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterData");
        }
    }
}
