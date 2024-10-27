using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "paymentMethod",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentMethod",
                table: "Bookings");
        }
    }
}
