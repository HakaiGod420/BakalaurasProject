using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class gameboardv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardTypes",
                columns: table => new
                {
                    BoardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
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
                name: "BoardGameEntityBoardTypeEntity",
                columns: table => new
                {
                    BoardTypesBoardTypeId = table.Column<int>(type: "int", nullable: false),
                    BoardsBoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameEntityBoardTypeEntity", x => new { x.BoardTypesBoardTypeId, x.BoardsBoardGameId });
                    table.ForeignKey(
                        name: "FK_BoardGameEntityBoardTypeEntity_BoardGames_BoardsBoardGameId",
                        column: x => x.BoardsBoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameEntityBoardTypeEntity_BoardTypes_BoardTypesBoardTypeId",
                        column: x => x.BoardTypesBoardTypeId,
                        principalTable: "BoardTypes",
                        principalColumn: "BoardTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameEntityCategoryEntity",
                columns: table => new
                {
                    BoardsBoardGameId = table.Column<int>(type: "int", nullable: false),
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameEntityCategoryEntity", x => new { x.BoardsBoardGameId, x.CategoriesCategoryId });
                    table.ForeignKey(
                        name: "FK_BoardGameEntityCategoryEntity_BoardGames_BoardsBoardGameId",
                        column: x => x.BoardsBoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameEntityCategoryEntity_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
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
                    BoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "BoardGameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AditionalFiles_BoardGameId",
                table: "AditionalFiles",
                column: "BoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameEntityBoardTypeEntity_BoardsBoardGameId",
                table: "BoardGameEntityBoardTypeEntity",
                column: "BoardsBoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameEntityCategoryEntity_CategoriesCategoryId",
                table: "BoardGameEntityCategoryEntity",
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
                name: "IX_Images_BoardGameId",
                table: "Images",
                column: "BoardGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AditionalFiles");

            migrationBuilder.DropTable(
                name: "BoardGameEntityBoardTypeEntity");

            migrationBuilder.DropTable(
                name: "BoardGameEntityCategoryEntity");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "BoardTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "TableBoardStates");
        }
    }
}
