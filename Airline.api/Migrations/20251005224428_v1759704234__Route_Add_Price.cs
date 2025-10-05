using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.Migrations
{
    /// <inheritdoc />
    public partial class v1759704234__Route_Add_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Routes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Routes");
        }
    }
}
