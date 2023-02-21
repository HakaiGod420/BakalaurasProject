using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class adduserstatesenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStateId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserStateId",
                table: "Users",
                column: "UserStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserStates_UserStateId",
                table: "Users",
                column: "UserStateId",
                principalTable: "UserStates",
                principalColumn: "UserStateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserStates_UserStateId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserStates");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserStateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserStateId",
                table: "Users");
        }
    }
}
