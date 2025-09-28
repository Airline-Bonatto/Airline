using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineAPI.Airline.api.Migrations;

/// <inheritdoc />
public partial class UT_Routes_Add_Departure_Arrival : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Arrival",
            table: "Route",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "Departure",
            table: "Route",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Arrival",
            table: "Route");

        migrationBuilder.DropColumn(
            name: "Departure",
            table: "Route");
    }
}
