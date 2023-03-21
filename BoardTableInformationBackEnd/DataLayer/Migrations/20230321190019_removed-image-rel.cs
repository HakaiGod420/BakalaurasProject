using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class removedimagerel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_BoardGames_BoardGameId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "BoardGameId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_BoardGames_BoardGameId",
                table: "Images",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "BoardGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_BoardGames_BoardGameId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "BoardGameId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_BoardGames_BoardGameId",
                table: "Images",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "BoardGameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
