using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.Migrations
{
    /// <inheritdoc />
    public partial class v1759760817__Remove_Route_From_Aircraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_AircraftID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "AircraftID",
                table: "Routes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AircraftID",
                table: "Routes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_AircraftID",
                table: "Routes",
                column: "AircraftID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Aircrafts_AircraftID",
                table: "Routes",
                column: "AircraftID",
                principalTable: "Aircrafts",
                principalColumn: "AircraftID");
        }
    }
}
