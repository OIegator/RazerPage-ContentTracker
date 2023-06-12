using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedMonthToUserGame2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "UserGames");
        }
    }
}
