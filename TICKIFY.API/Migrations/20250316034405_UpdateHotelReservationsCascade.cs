using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TICKIFY.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotelReservationsCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
