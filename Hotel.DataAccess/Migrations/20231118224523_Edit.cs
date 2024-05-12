using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountApplied",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "HasPreviousBooking",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "DiscountApplied",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGuests",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPreviousBooking",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
