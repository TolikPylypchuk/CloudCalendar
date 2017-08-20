using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCalendar.Data.Migrations
{
    public partial class ManagerToHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Lecturers");

            migrationBuilder.AddColumn<bool>(
                name: "IsHead",
                table: "Lecturers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHead",
                table: "Lecturers");

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Lecturers",
                nullable: false,
                defaultValue: false);
        }
    }
}
