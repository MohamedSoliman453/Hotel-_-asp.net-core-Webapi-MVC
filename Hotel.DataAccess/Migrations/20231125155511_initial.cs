using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelBranch",
                columns: table => new
                {
                    HotelBranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelBranch", x => x.HotelBranchID);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfPersons = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelBranchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_HotelBranch_HotelBranchID",
                        column: x => x.HotelBranchID,
                        principalTable: "HotelBranch",
                        principalColumn: "HotelBranchID");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelBranchID = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_HotelBranch_HotelBranchID",
                        column: x => x.HotelBranchID,
                        principalTable: "HotelBranch",
                        principalColumn: "HotelBranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomBookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookings", x => new { x.BookingID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_RoomBookings_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomBookings_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HotelBranch",
                columns: new[] { "HotelBranchID", "Address", "BranchName" },
                values: new object[,]
                {
                    { 1, "123 Downtown Ave", "Downtown Hotel" },
                    { 2, "1 Airport Rd", "Airport Hotel" },
                    { 3, "100 Beach Dr", "Beachside Hotel" },
                    { 4, "200 Mountain St", "Mountain View Hotel" },
                    { 5, "500 City Center Blvd", "City Center Hotel" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeID", "Description", "Price", "TypeName" },
                values: new object[,]
                {
                    { 1, "Single Room", 100m, "Single" },
                    { 2, "Double Room", 150m, "Double" },
                    { 3, "Triple Room", 180m, "Triple" },
                    { 4, "Suite Room", 220m, "Suite" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomID", "HotelBranchID", "RoomTypeID", "Status" },
                values: new object[,]
                {
                    { 1, 1, 1, "Available" },
                    { 2, 1, 1, "Available" },
                    { 3, 1, 1, "Available" },
                    { 4, 1, 1, "Available" },
                    { 5, 1, 2, "Available" },
                    { 6, 1, 2, "Available" },
                    { 7, 1, 2, "Available" },
                    { 8, 1, 2, "Available" },
                    { 9, 1, 3, "Available" },
                    { 10, 1, 3, "Available" },
                    { 11, 1, 3, "Available" },
                    { 12, 1, 3, "Available" },
                    { 13, 1, 4, "Available" },
                    { 14, 1, 4, "Available" },
                    { 15, 1, 4, "Available" },
                    { 16, 1, 4, "Available" },
                    { 17, 2, 1, "Available" },
                    { 18, 2, 1, "Available" },
                    { 19, 2, 1, "Available" },
                    { 20, 2, 1, "Available" },
                    { 21, 2, 2, "Available" },
                    { 22, 2, 2, "Available" },
                    { 23, 2, 2, "Available" },
                    { 24, 2, 2, "Available" },
                    { 25, 2, 3, "Available" },
                    { 26, 2, 3, "Available" },
                    { 27, 2, 3, "Available" },
                    { 28, 2, 3, "Available" },
                    { 29, 2, 4, "Available" },
                    { 30, 2, 4, "Available" },
                    { 31, 2, 4, "Available" },
                    { 32, 2, 4, "Available" },
                    { 33, 3, 1, "Available" },
                    { 34, 3, 1, "Available" },
                    { 35, 3, 1, "Available" },
                    { 36, 3, 1, "Available" },
                    { 37, 3, 2, "Available" },
                    { 38, 3, 2, "Available" },
                    { 39, 3, 2, "Available" },
                    { 40, 3, 2, "Available" },
                    { 41, 3, 3, "Available" },
                    { 42, 3, 3, "Available" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomID", "HotelBranchID", "RoomTypeID", "Status" },
                values: new object[,]
                {
                    { 43, 3, 3, "Available" },
                    { 44, 3, 3, "Available" },
                    { 45, 3, 4, "Available" },
                    { 46, 3, 4, "Available" },
                    { 47, 3, 4, "Available" },
                    { 48, 3, 4, "Available" },
                    { 49, 4, 1, "Available" },
                    { 50, 4, 1, "Available" },
                    { 51, 4, 1, "Available" },
                    { 52, 4, 1, "Available" },
                    { 53, 4, 2, "Available" },
                    { 54, 4, 2, "Available" },
                    { 55, 4, 2, "Available" },
                    { 56, 4, 2, "Available" },
                    { 57, 4, 3, "Available" },
                    { 58, 4, 3, "Available" },
                    { 59, 4, 3, "Available" },
                    { 60, 4, 3, "Available" },
                    { 61, 4, 4, "Available" },
                    { 62, 4, 4, "Available" },
                    { 63, 4, 4, "Available" },
                    { 64, 4, 4, "Available" },
                    { 65, 5, 1, "Available" },
                    { 66, 5, 1, "Available" },
                    { 67, 5, 1, "Available" },
                    { 68, 5, 1, "Available" },
                    { 69, 5, 2, "Available" },
                    { 70, 5, 2, "Available" },
                    { 71, 5, 2, "Available" },
                    { 72, 5, 2, "Available" },
                    { 73, 5, 3, "Available" },
                    { 74, 5, 3, "Available" },
                    { 75, 5, 3, "Available" },
                    { 76, 5, 3, "Available" },
                    { 77, 5, 4, "Available" },
                    { 78, 5, 4, "Available" },
                    { 79, 5, 4, "Available" },
                    { 80, 5, 4, "Available" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerID",
                table: "Bookings",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelBranchID",
                table: "Bookings",
                column: "HotelBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RoomID",
                table: "RoomBookings",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelBranchID",
                table: "Rooms",
                column: "HotelBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RoomBookings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "HotelBranch");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
