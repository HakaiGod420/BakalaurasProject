using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class reviewsv15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedBoardGameId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SelectedBoardGameId",
                table: "Reviews",
                column: "SelectedBoardGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_BoardGames_SelectedBoardGameId",
                table: "Reviews",
                column: "SelectedBoardGameId",
                principalTable: "BoardGames",
                principalColumn: "BoardGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_BoardGames_SelectedBoardGameId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_SelectedBoardGameId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "SelectedBoardGameId",
                table: "Reviews");
        }
    }
}
