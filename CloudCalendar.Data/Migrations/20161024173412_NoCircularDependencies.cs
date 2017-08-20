using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCalendar.Data.Migrations
{
    public partial class NoCircularDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Lecturers_ManagerId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Lecturers_DeanId",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Students_GroupLeaderId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Students_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupLeaderId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_DeanId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "GroupLeaderId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DeanId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroupLeader",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDean",
                table: "Lecturers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Lecturers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsGroupLeader",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDean",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Lecturers");

            migrationBuilder.AddColumn<int>(
                name: "GroupLeaderId",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeanId",
                table: "Faculties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupLeaderId",
                table: "Groups",
                column: "GroupLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_DeanId",
                table: "Faculties",
                column: "DeanId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Lecturers_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Lecturers_DeanId",
                table: "Faculties",
                column: "DeanId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Students_GroupLeaderId",
                table: "Groups",
                column: "GroupLeaderId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
