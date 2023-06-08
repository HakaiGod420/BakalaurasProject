using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActiveGame_ActiveGames_ActiveGamesParcipatorsActiveGameId",
                table: "UserActiveGame");

            migrationBuilder.RenameColumn(
                name: "ActiveGamesParcipatorsActiveGameId",
                table: "UserActiveGame",
                newName: "ActiveGamesParticipantsActiveGameId");

            migrationBuilder.RenameColumn(
                name: "Thubnail_Location",
                table: "BoardGames",
                newName: "Thumbnail_Location");

            migrationBuilder.RenameColumn(
                name: "RegistredPlayerCount",
                table: "ActiveGames",
                newName: "RegisteredPlayerCount");

            migrationBuilder.RenameColumn(
                name: "Map_Y_Cords",
                table: "ActiveGames",
                newName: "Map_Y_Coords");

            migrationBuilder.RenameColumn(
                name: "Map_X_Cords",
                table: "ActiveGames",
                newName: "Map_X_Coords");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActiveGame_ActiveGames_ActiveGamesParticipantsActiveGameId",
                table: "UserActiveGame",
                column: "ActiveGamesParticipantsActiveGameId",
                principalTable: "ActiveGames",
                principalColumn: "ActiveGameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActiveGame_ActiveGames_ActiveGamesParticipantsActiveGameId",
                table: "UserActiveGame");

            migrationBuilder.RenameColumn(
                name: "ActiveGamesParticipantsActiveGameId",
                table: "UserActiveGame",
                newName: "ActiveGamesParcipatorsActiveGameId");

            migrationBuilder.RenameColumn(
                name: "Thumbnail_Location",
                table: "BoardGames",
                newName: "Thubnail_Location");

            migrationBuilder.RenameColumn(
                name: "RegisteredPlayerCount",
                table: "ActiveGames",
                newName: "RegistredPlayerCount");

            migrationBuilder.RenameColumn(
                name: "Map_Y_Coords",
                table: "ActiveGames",
                newName: "Map_Y_Cords");

            migrationBuilder.RenameColumn(
                name: "Map_X_Coords",
                table: "ActiveGames",
                newName: "Map_X_Cords");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActiveGame_ActiveGames_ActiveGamesParcipatorsActiveGameId",
                table: "UserActiveGame",
                column: "ActiveGamesParcipatorsActiveGameId",
                principalTable: "ActiveGames",
                principalColumn: "ActiveGameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
