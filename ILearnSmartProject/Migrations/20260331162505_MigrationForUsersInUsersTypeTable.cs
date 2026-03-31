using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILearnSmartProject.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForUsersInUsersTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersTypeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UsersTypeId",
                table: "Users",
                column: "UsersTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersTypes_UsersTypeId",
                table: "Users",
                column: "UsersTypeId",
                principalTable: "UsersTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersTypes_UsersTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UsersTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersTypeId",
                table: "Users");
        }
    }
}
