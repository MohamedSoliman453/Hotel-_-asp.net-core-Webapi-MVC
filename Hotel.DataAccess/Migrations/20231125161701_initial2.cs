using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_HotelBranch_HotelBranchID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_HotelBranchID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HotelBranchID",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelBranchID",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelBranchID",
                table: "Bookings",
                column: "HotelBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_HotelBranch_HotelBranchID",
                table: "Bookings",
                column: "HotelBranchID",
                principalTable: "HotelBranch",
                principalColumn: "HotelBranchID");
        }
    }
}
