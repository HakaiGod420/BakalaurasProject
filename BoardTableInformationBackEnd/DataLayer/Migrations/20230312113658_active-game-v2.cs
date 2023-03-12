using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class activegamev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveGameStates",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveGameStates", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "ActiveGames",
                columns: table => new
                {
                    ActiveGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayersNeed = table.Column<int>(type: "int", nullable: false),
                    RegistredPlayerCount = table.Column<int>(type: "int", nullable: false),
                    ActiveGameStateId = table.Column<int>(type: "int", nullable: false),
                    ActiveGameStateStateId = table.Column<int>(type: "int", nullable: false),
                    Map_X_Cords = table.Column<float>(type: "real", nullable: false),
                    Map_Y_Cords = table.Column<float>(type: "real", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveGames", x => x.ActiveGameId);
                    table.ForeignKey(
                        name: "FK_ActiveGames_ActiveGameStates_ActiveGameStateStateId",
                        column: x => x.ActiveGameStateStateId,
                        principalTable: "ActiveGameStates",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveGames_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveGames_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserActiveGame",
                columns: table => new
                {
                    ActiveGamesParcipatorsActiveGameId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActiveGame", x => new { x.ActiveGamesParcipatorsActiveGameId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserActiveGame_ActiveGames_ActiveGamesParcipatorsActiveGameId",
                        column: x => x.ActiveGamesParcipatorsActiveGameId,
                        principalTable: "ActiveGames",
                        principalColumn: "ActiveGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActiveGame_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_ActiveGameStateStateId",
                table: "ActiveGames",
                column: "ActiveGameStateStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_AddressId",
                table: "ActiveGames",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_CreatorId",
                table: "ActiveGames",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActiveGame_UsersUserId",
                table: "UserActiveGame",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActiveGame");

            migrationBuilder.DropTable(
                name: "ActiveGames");

            migrationBuilder.DropTable(
                name: "ActiveGameStates");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
