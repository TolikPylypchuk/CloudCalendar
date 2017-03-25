using Microsoft.EntityFrameworkCore.Migrations;

namespace InterlogicProject.DAL.Migrations
{
    public partial class HomeworkEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HomeworkEnabled",
                table: "Classes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeworkEnabled",
                table: "Classes");
        }
    }
}
