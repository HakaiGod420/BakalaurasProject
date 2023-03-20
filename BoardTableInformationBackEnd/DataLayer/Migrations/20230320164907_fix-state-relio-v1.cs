using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class fixstatereliov1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveGames_ActiveGameStates_ActiveGameStateStateId",
                table: "ActiveGames");

            migrationBuilder.DropIndex(
                name: "IX_ActiveGames_ActiveGameStateStateId",
                table: "ActiveGames");

            migrationBuilder.DropColumn(
                name: "ActiveGameStateId",
                table: "ActiveGames");

            migrationBuilder.DropColumn(
                name: "ActiveGameStateStateId",
                table: "ActiveGames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveGameStateId",
                table: "ActiveGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActiveGameStateStateId",
                table: "ActiveGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_ActiveGameStateStateId",
                table: "ActiveGames",
                column: "ActiveGameStateStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveGames_ActiveGameStates_ActiveGameStateStateId",
                table: "ActiveGames",
                column: "ActiveGameStateStateId",
                principalTable: "ActiveGameStates",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
