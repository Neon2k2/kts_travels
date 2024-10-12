﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kts_travels.Migrations
{
    /// <inheritdoc />
    public partial class modifiedTripLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "TripLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "TripLog");
        }
    }
}