using Microsoft.EntityFrameworkCore.Migrations;

namespace InterlogicProject.DAL.Migrations
{
	public partial class ClassInNotification : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "ClassId",
				table: "Notifications",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_ClassId",
				table: "Notifications",
				column: "ClassId");

			migrationBuilder.AddForeignKey(
				name: "FK_Notifications_Classes_ClassId",
				table: "Notifications",
				column: "ClassId",
				principalTable: "Classes",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Notifications_Classes_ClassId",
				table: "Notifications");

			migrationBuilder.DropIndex(
				name: "IX_Notifications_ClassId",
				table: "Notifications");

			migrationBuilder.DropColumn(
				name: "ClassId",
				table: "Notifications");
		}
	}
}
