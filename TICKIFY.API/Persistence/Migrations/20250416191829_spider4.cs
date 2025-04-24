using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TICKIFY.API.Migrations
{
    /// <inheritdoc />
    public partial class spider4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_HotelReservations_HotelReservationId",
                table: "ReservationDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReservationDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReservationDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_HotelReservations_HotelReservationId",
                table: "ReservationDetails",
                column: "HotelReservationId",
                principalTable: "HotelReservations",
                principalColumn: "HotelReservationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_HotelReservations_HotelReservationId",
                table: "ReservationDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReservationDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ReservationDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_HotelReservations_HotelReservationId",
                table: "ReservationDetails",
                column: "HotelReservationId",
                principalTable: "HotelReservations",
                principalColumn: "HotelReservationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
