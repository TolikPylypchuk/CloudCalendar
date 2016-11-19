using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterlogicProject.DAL.Migrations
{
	public partial class Classes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Subjects",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Subjects", x => x.Id);
				});

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

			migrationBuilder.CreateTable(
				name: "Classes",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Building = table.Column<string>(nullable: false),
					Classroom = table.Column<string>(nullable: false),
					DateTime = table.Column<DateTime>(nullable: false),
					SubjectId = table.Column<int>(nullable: false),
					Type = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Classes", x => x.Id);
					table.ForeignKey(
						name: "FK_Classes_GroupSubjects_SubjectId",
						column: x => x.SubjectId,
						principalTable: "GroupSubjects",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Classes_SubjectId",
				table: "Classes",
				column: "SubjectId");

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

			migrationBuilder.CreateIndex(
				name: "IX_Subjects_Name",
				table: "Subjects",
				column: "Name",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Classes");

			migrationBuilder.DropTable(
				name: "GroupSubjects");

			migrationBuilder.DropTable(
				name: "Subjects");
		}
	}
}
