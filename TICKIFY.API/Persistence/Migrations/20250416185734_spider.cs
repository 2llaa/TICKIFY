using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TICKIFY.API.Migrations
{
    /// <inheritdoc />
    public partial class Spider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "HotelReservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HotelReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HotelReservations");
        }
    }
}
