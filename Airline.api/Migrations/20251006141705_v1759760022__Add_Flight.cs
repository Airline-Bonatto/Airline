using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Airline.Migrations
{
    /// <inheritdoc />
    public partial class v1759760022__Add_Flight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Arrival",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Departure",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Routes");

            migrationBuilder.AlterColumn<int>(
                name: "AircraftID",
                table: "Routes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RouteID = table.Column<int>(type: "integer", nullable: false),
                    AircraftID = table.Column<int>(type: "integer", nullable: false),
                    Departure = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Arrival = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_AircraftID",
                        column: x => x.AircraftID,
                        principalTable: "Aircrafts",
                        principalColumn: "AircraftID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftID",
                table: "Flights",
                column: "AircraftID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_RouteID",
                table: "Flights",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes",
                column: "AircraftID",
                principalTable: "Aircrafts",
                principalColumn: "AircraftID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.AlterColumn<int>(
                name: "AircraftID",
                table: "Routes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Arrival",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Departure",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Routes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes",
                column: "AircraftID",
                principalTable: "Aircrafts",
                principalColumn: "AircraftID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
