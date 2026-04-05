using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILearnSmartProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseTableCompletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "CoursesUserPurchases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "CoursesUserPurchases");
        }
    }
}
