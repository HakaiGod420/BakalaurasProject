using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class addedlinkwithgameboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardGameId",
                table: "ActiveGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_BoardGameId",
                table: "ActiveGames",
                column: "BoardGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveGames_BoardGames_BoardGameId",
                table: "ActiveGames",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "BoardGameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveGames_BoardGames_BoardGameId",
                table: "ActiveGames");

            migrationBuilder.DropIndex(
                name: "IX_ActiveGames_BoardGameId",
                table: "ActiveGames");

            migrationBuilder.DropColumn(
                name: "BoardGameId",
                table: "ActiveGames");
        }
    }
}
