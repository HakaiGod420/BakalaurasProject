using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class usersAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAddressEntity_AddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity");

            migrationBuilder.RenameTable(
                name: "UserAddressEntity",
                newName: "UserAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress",
                column: "UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAddress_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "UserAddress",
                principalColumn: "UserAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAddress_AddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress");

            migrationBuilder.RenameTable(
                name: "UserAddress",
                newName: "UserAddressEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity",
                column: "UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAddressEntity_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "UserAddressEntity",
                principalColumn: "UserAddressId");
        }
    }
}
