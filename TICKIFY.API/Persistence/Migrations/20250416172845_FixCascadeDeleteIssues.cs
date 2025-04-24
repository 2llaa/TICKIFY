using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TICKIFY.API.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Drivers_DriverId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Drivers_DriverId",
                table: "HotelReservations",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Drivers_DriverId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Drivers_DriverId",
                table: "HotelReservations",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
