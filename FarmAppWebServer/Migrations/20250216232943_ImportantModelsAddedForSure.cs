using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    /// <inheritdoc />
    public partial class ImportantModelsAddedForSure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantTypesDataValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorePrice = table.Column<int>(type: "int", nullable: false),
                    HarvestedPrice = table.Column<int>(type: "int", nullable: false),
                    SecondsToGrowOneState = table.Column<int>(type: "int", nullable: false),
                    MaxGrowState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTypesDataValues", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayerDataValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    FarmLevel = table.Column<int>(type: "int", nullable: false),
                    CurrentPlayerLevel = table.Column<int>(type: "int", nullable: false),
                    CurrentPlayerXP = table.Column<long>(type: "bigint", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDataValues", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayersPlantsInstanceValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    InitialPlantTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GrowStateInSeconds = table.Column<int>(type: "int", nullable: false),
                    PotIndex = table.Column<int>(type: "int", nullable: false),
                    CurrentGrowState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersPlantsInstanceValues", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantTypesDataValues");

            migrationBuilder.DropTable(
                name: "PlayerDataValues");

            migrationBuilder.DropTable(
                name: "PlayersPlantsInstanceValues");
        }
    }
}
