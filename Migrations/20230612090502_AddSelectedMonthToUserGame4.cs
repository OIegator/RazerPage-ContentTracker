using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedMonthToUserGame4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE UserGames SET Month = 'January' WHERE Month IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "UserGames",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true,
                defaultValue: "January");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "UserGames",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: false,
                oldDefaultValue: "January");

        }
    }
}
