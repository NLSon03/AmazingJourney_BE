using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatebooking2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Bookings");
        }
    }
}
