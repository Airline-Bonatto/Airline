using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.Migrations
{
    /// <inheritdoc />
    public partial class v1761521089__Add_OwnerDocument_On_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerDocument",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerDocument",
                table: "Tickets");
        }
    }
}
