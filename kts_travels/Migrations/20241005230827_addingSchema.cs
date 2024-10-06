using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kts_travels.Migrations
{
    /// <inheritdoc />
    public partial class addingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DieselEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VehicleNo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DieselLiters = table.Column<double>(type: "REAL", nullable: false),
                    StartingKm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieselEntries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DieselEntries");
        }
    }
}
