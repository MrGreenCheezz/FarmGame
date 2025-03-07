using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPlayerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastToken",
                table: "PlayerDataValues",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastToken",
                table: "PlayerDataValues");
        }
    }
}
