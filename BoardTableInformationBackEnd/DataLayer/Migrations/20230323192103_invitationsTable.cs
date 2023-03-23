using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class invitationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvitationStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentInvitations",
                columns: table => new
                {
                    SentInvitationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectedActiveGameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InvitationStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentInvitations", x => x.SentInvitationId);
                    table.ForeignKey(
                        name: "FK_SentInvitations_ActiveGames_SelectedActiveGameId",
                        column: x => x.SelectedActiveGameId,
                        principalTable: "ActiveGames",
                        principalColumn: "ActiveGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SentInvitations_InvitationStates_InvitationStateId",
                        column: x => x.InvitationStateId,
                        principalTable: "InvitationStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SentInvitations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentInvitations_InvitationStateId",
                table: "SentInvitations",
                column: "InvitationStateId");

            migrationBuilder.CreateIndex(
                name: "IX_SentInvitations_SelectedActiveGameId",
                table: "SentInvitations",
                column: "SelectedActiveGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SentInvitations_UserId",
                table: "SentInvitations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SentInvitations");

            migrationBuilder.DropTable(
                name: "InvitationStates");
        }
    }
}
