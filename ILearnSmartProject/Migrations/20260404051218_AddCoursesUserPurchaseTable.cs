using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILearnSmartProject.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursesUserPurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesUserPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesUserPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesUserPurchases_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesUserPurchases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesUserPurchases_CourseId",
                table: "CoursesUserPurchases",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesUserPurchases_UserId",
                table: "CoursesUserPurchases",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesUserPurchases");
        }
    }
}
