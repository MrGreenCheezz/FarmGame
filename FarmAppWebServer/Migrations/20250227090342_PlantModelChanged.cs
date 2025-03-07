using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    /// <inheritdoc />
    public partial class PlantModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialPlantTime",
                table: "PlayersPlantsInstanceValues",
                newName: "LastClientInteraction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastClientInteraction",
                table: "PlayersPlantsInstanceValues",
                newName: "InitialPlantTime");
        }
    }
}
