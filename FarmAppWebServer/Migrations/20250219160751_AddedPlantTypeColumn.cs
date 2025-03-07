using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlantTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantType",
                table: "PlayersPlantsInstanceValues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantType",
                table: "PlayersPlantsInstanceValues");
        }
    }
}
