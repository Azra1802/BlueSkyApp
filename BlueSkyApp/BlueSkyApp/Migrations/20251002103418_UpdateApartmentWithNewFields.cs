using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueSkyApp.Migrations
{
    public partial class UpdateApartmentWithNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Apartments");
        }
    }
}
