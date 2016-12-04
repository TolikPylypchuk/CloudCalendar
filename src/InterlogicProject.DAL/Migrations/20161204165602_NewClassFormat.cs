using System;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterlogicProject.DAL.Migrations
{
	public partial class NewClassFormat : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Classes_GroupSubjects_SubjectId",
				table: "Classes");

			migrationBuilder.DropColumn(
				name: "Building",
				table: "Classes");

			migrationBuilder.DropColumn(
				name: "Classroom",
				table: "Classes");

			migrationBuilder.DropTable(
				name: "GroupSubjects");

			migrationBuilder.CreateTable(
				name: "ClassPlaces",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Building = table.Column<string>(nullable: false),
					ClassId = table.Column<int>(nullable: false),
					Classroom = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ClassPlaces", x => x.Id);
					table.ForeignKey(
						name: "FK_ClassPlaces_Classes_ClassId",
						column: x => x.ClassId,
						principalTable: "Classes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "LecturersToClasses",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ClassId = table.Column<int>(nullable: false),
					LecturerId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_LecturersToClasses", x => x.Id);
					table.ForeignKey(
						name: "FK_LecturersToClasses_Classes_ClassId",
						column: x => x.ClassId,
						principalTable: "Classes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_LecturersToClasses_Lecturers_LecturerId",
						column: x => x.LecturerId,
						principalTable: "Lecturers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ClassPlaces_ClassId",
				table: "ClassPlaces",
				column: "ClassId");

			migrationBuilder.CreateIndex(
				name: "IX_LecturersToClasses_ClassId",
				table: "LecturersToClasses",
				column: "ClassId");

			migrationBuilder.CreateIndex(
				name: "IX_LecturersToClasses_LecturerId",
				table: "LecturersToClasses",
				column: "LecturerId");

			migrationBuilder.AddForeignKey(
				name: "FK_Classes_Subjects_SubjectId",
				table: "Classes",
				column: "SubjectId",
				principalTable: "Subjects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Classes_Subjects_SubjectId",
				table: "Classes");

			migrationBuilder.DropTable(
				name: "ClassPlaces");

			migrationBuilder.DropTable(
				name: "LecturersToClasses");

			migrationBuilder.CreateTable(
				name: "GroupSubjects",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					GroupId = table.Column<int>(nullable: false),
					LecturerId = table.Column<int>(nullable: false),
					SubjectId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GroupSubjects", x => x.Id);
					table.ForeignKey(
						name: "FK_GroupSubjects_Groups_GroupId",
						column: x => x.GroupId,
						principalTable: "Groups",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_GroupSubjects_Lecturers_LecturerId",
						column: x => x.LecturerId,
						principalTable: "Lecturers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_GroupSubjects_Subjects_SubjectId",
						column: x => x.SubjectId,
						principalTable: "Subjects",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.AddColumn<string>(
				name: "Building",
				table: "Classes",
				nullable: false,
				defaultValue: String.Empty);

			migrationBuilder.AddColumn<string>(
				name: "Classroom",
				table: "Classes",
				nullable: false,
				defaultValue: String.Empty);

			migrationBuilder.CreateIndex(
				name: "IX_GroupSubjects_GroupId",
				table: "GroupSubjects",
				column: "GroupId");

			migrationBuilder.CreateIndex(
				name: "IX_GroupSubjects_LecturerId",
				table: "GroupSubjects",
				column: "LecturerId");

			migrationBuilder.CreateIndex(
				name: "IX_GroupSubjects_SubjectId",
				table: "GroupSubjects",
				column: "SubjectId");

			migrationBuilder.AddForeignKey(
				name: "FK_Classes_GroupSubjects_SubjectId",
				table: "Classes",
				column: "SubjectId",
				principalTable: "GroupSubjects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
