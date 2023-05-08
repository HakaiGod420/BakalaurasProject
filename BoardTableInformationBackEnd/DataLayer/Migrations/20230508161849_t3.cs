using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class t3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveGames_ActiveGameStates_InvitationStateActiveGameStateId",
                table: "ActiveGames");

            migrationBuilder.DropIndex(
                name: "IX_ActiveGames_InvitationStateActiveGameStateId",
                table: "ActiveGames");

            migrationBuilder.DropColumn(
                name: "InvitationStateActiveGameStateId",
                table: "ActiveGames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvitationStateActiveGameStateId",
                table: "ActiveGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_InvitationStateActiveGameStateId",
                table: "ActiveGames",
                column: "InvitationStateActiveGameStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveGames_ActiveGameStates_InvitationStateActiveGameStateId",
                table: "ActiveGames",
                column: "InvitationStateActiveGameStateId",
                principalTable: "ActiveGameStates",
                principalColumn: "ActiveGameStateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
