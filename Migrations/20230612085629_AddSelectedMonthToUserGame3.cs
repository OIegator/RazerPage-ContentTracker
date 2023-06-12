using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedMonthToUserGame3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGames");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
