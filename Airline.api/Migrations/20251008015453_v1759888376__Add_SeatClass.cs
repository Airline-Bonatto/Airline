using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airline.Migrations
{
    /// <inheritdoc />
    public partial class v1759888376__Add_SeatClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeatClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatClasses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SeatClasses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 0, "Economic" },
                    { 1, "Executive" },
                    { 2, "FirstClass" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatClasses");
        }
    }
}
