using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacketData",
                columns: table => new
                {
                    Serial = table.Column<string>(type: "TEXT", nullable: false),
                    Datetime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EnvTemperature = table.Column<double>(type: "REAL", nullable: true),
                    Humidity = table.Column<double>(type: "REAL", nullable: true),
                    DewPoint = table.Column<double>(type: "REAL", nullable: true),
                    Pressure_hPa = table.Column<double>(type: "REAL", nullable: true),
                    Pressure_mm_hg = table.Column<double>(type: "REAL", nullable: true),
                    WindSpeed = table.Column<double>(type: "REAL", nullable: true),
                    WindDirection = table.Column<double>(type: "REAL", nullable: true),
                    WindVSound = table.Column<double>(type: "REAL", nullable: true),
                    PrecipitationType = table.Column<int>(type: "INTEGER", nullable: true),
                    PrecipitationIntensity = table.Column<double>(type: "REAL", nullable: true),
                    PrecipitationQuantity = table.Column<double>(type: "REAL", nullable: true),
                    PrecipitationElaps = table.Column<int>(type: "INTEGER", nullable: true),
                    PrecipitationPeriod = table.Column<int>(type: "INTEGER", nullable: true),
                    SupplyVoltage = table.Column<double>(type: "REAL", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true),
                    Altitude = table.Column<int>(type: "INTEGER", nullable: true),
                    KSP = table.Column<int>(type: "INTEGER", nullable: true),
                    AccelerationStDev = table.Column<double>(type: "REAL", nullable: true),
                    Roll = table.Column<double>(type: "REAL", nullable: true),
                    Pitch = table.Column<double>(type: "REAL", nullable: true),
                    WeAreFine = table.Column<int>(type: "INTEGER", nullable: true),
                    VisibleRange = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketData", x => x.Serial);
                });

            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    Serial = table.Column<string>(type: "TEXT", nullable: false),
                    PacketSerial = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.Serial);
                    table.ForeignKey(
                        name: "FK_SensorData_PacketData_PacketSerial",
                        column: x => x.PacketSerial,
                        principalTable: "PacketData",
                        principalColumn: "Serial");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_PacketSerial",
                table: "SensorData",
                column: "PacketSerial");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropTable(
                name: "PacketData");
        }
    }
}
