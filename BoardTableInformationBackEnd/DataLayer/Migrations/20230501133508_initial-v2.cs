using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class initialv2 : Migration
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
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "BoardTypes",
                columns: table => new
                {
                    BoardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardTypes", x => x.BoardTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

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
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TableBoardStates",
                columns: table => new
                {
                    TableBoardStateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBoardStates", x => x.TableBoardStateId);
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    UserAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Map_X_Coords = table.Column<double>(type: "float", nullable: true),
                    Map_Y_Coords = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.UserAddressId);
                });

            migrationBuilder.CreateTable(
                name: "UserStates",
                columns: table => new
                {
                    UserStateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStates", x => x.UserStateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserStateId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTimeConnection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableInvitationNotifications = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "UserAddress",
                        principalColumn: "UserAddressId");
                    table.ForeignKey(
                        name: "FK_Users_UserStates_UserStateId",
                        column: x => x.UserStateId,
                        principalTable: "UserStates",
                        principalColumn: "UserStateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    BoardGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PlayerCount = table.Column<int>(type: "int", nullable: false),
                    PlayingTime = table.Column<int>(type: "int", nullable: true),
                    PlayableAge = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rules = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Thubnail_Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TableBoardStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.BoardGameId);
                    table.ForeignKey(
                        name: "FK_BoardGames_TableBoardStates_TableBoardStateId",
                        column: x => x.TableBoardStateId,
                        principalTable: "TableBoardStates",
                        principalColumn: "TableBoardStateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveGames",
                columns: table => new
                {
                    ActiveGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayersNeed = table.Column<int>(type: "int", nullable: false),
                    RegistredPlayerCount = table.Column<int>(type: "int", nullable: false),
                    InvitationStateId = table.Column<int>(type: "int", nullable: false),
                    Map_X_Cords = table.Column<float>(type: "real", nullable: false),
                    Map_Y_Cords = table.Column<float>(type: "real", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    MeetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveGames", x => x.ActiveGameId);
                    table.ForeignKey(
                        name: "FK_ActiveGames_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveGames_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActiveGames_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AditionalFiles",
                columns: table => new
                {
                    AditionalFilesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AditionalFiles", x => x.AditionalFilesId);
                    table.ForeignKey(
                        name: "FK_AditionalFiles_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameCategory",
                columns: table => new
                {
                    BoardsBoardGameId = table.Column<int>(type: "int", nullable: false),
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameCategory", x => new { x.BoardsBoardGameId, x.CategoriesCategoryId });
                    table.ForeignKey(
                        name: "FK_BoardGameCategory_BoardGames_BoardsBoardGameId",
                        column: x => x.BoardsBoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameCategory_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameType",
                columns: table => new
                {
                    BoardTypesBoardTypeId = table.Column<int>(type: "int", nullable: false),
                    BoardsBoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameType", x => new { x.BoardTypesBoardTypeId, x.BoardsBoardGameId });
                    table.ForeignKey(
                        name: "FK_BoardGameType_BoardGames_BoardsBoardGameId",
                        column: x => x.BoardsBoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameType_BoardTypes_BoardTypesBoardTypeId",
                        column: x => x.BoardTypesBoardTypeId,
                        principalTable: "BoardTypes",
                        principalColumn: "BoardTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BoardGameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WriterId = table.Column<int>(type: "int", nullable: false),
                    WriteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedBoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_BoardGames_SelectedBoardGameId",
                        column: x => x.SelectedBoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_WriterId",
                        column: x => x.WriterId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ActiveGames_AddressId",
                table: "ActiveGames",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_BoardGameId",
                table: "ActiveGames",
                column: "BoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveGames_CreatorId",
                table: "ActiveGames",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AditionalFiles_BoardGameId",
                table: "AditionalFiles",
                column: "BoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameCategory_CategoriesCategoryId",
                table: "BoardGameCategory",
                column: "CategoriesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_TableBoardStateId",
                table: "BoardGames",
                column: "TableBoardStateId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_UserId",
                table: "BoardGames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameType_BoardsBoardGameId",
                table: "BoardGameType",
                column: "BoardsBoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BoardGameId",
                table: "Images",
                column: "BoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SelectedBoardGameId",
                table: "Reviews",
                column: "SelectedBoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WriterId",
                table: "Reviews",
                column: "WriterId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserActiveGame_UsersUserId",
                table: "UserActiveGame",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserStateId",
                table: "Users",
                column: "UserStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveGameStates");

            migrationBuilder.DropTable(
                name: "AditionalFiles");

            migrationBuilder.DropTable(
                name: "BoardGameCategory");

            migrationBuilder.DropTable(
                name: "BoardGameType");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SentInvitations");

            migrationBuilder.DropTable(
                name: "UserActiveGame");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BoardTypes");

            migrationBuilder.DropTable(
                name: "InvitationStates");

            migrationBuilder.DropTable(
                name: "ActiveGames");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "TableBoardStates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "UserStates");
        }
    }
}
