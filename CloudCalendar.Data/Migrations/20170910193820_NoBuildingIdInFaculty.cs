using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCalendar.Data.Migrations
{
	public partial class NoBuildingIdInFaculty : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Faculties_Buildings_BuildingId",
				table: "Faculties");

			migrationBuilder.DropIndex(
				name: "UserNameIndex",
				table: "AspNetUsers");

			migrationBuilder.DropIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles");

			migrationBuilder.AlterColumn<int>(
				name: "BuildingId",
				table: "Faculties",
				type: "int",
				nullable: true,
				oldClrType: typeof(int));

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUserTokens_AspNetUsers_UserId",
				table: "AspNetUserTokens",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Faculties_Buildings_BuildingId",
				table: "Faculties",
				column: "BuildingId",
				principalTable: "Buildings",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUserTokens_AspNetUsers_UserId",
				table: "AspNetUserTokens");

			migrationBuilder.DropForeignKey(
				name: "FK_Faculties_Buildings_BuildingId",
				table: "Faculties");

			migrationBuilder.DropIndex(
				name: "UserNameIndex",
				table: "AspNetUsers");

			migrationBuilder.DropIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles");

			migrationBuilder.AlterColumn<int>(
				name: "BuildingId",
				table: "Faculties",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Faculties_Buildings_BuildingId",
				table: "Faculties",
				column: "BuildingId",
				principalTable: "Buildings",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
