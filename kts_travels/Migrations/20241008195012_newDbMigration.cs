using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kts_travels.Migrations
{
    /// <inheritdoc />
    public partial class newDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleNo = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "TripLog",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleNO = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    DieselLiters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartingKm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLog", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_TripLog_Sites_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripLog_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSummary",
                columns: table => new
                {
                    SummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SRNo = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TotalDaysFilledDiesel = table.Column<int>(type: "int", nullable: false),
                    TotalDiesel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningKms = table.Column<int>(type: "int", nullable: false),
                    ClosingKms = table.Column<int>(type: "int", nullable: false),
                    TotalKmRun = table.Column<int>(type: "int", nullable: false),
                    Average = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSummary", x => x.SummaryId);
                    table.ForeignKey(
                        name: "FK_VehicleSummary_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripLog_LocationId",
                table: "TripLog",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TripLog_VehicleId",
                table: "TripLog",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSummary_VehicleId",
                table: "VehicleSummary",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripLog");

            migrationBuilder.DropTable(
                name: "VehicleSummary");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
