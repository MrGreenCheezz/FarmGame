using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    /// <inheritdoc />
    public partial class farmBaseCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseFarmCost",
                table: "PlayerDataValues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseFarmCost",
                table: "PlayerDataValues");
        }
    }
}
