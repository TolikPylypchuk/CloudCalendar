using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudCalendar.Data.Migrations
{
    public partial class GroupsClassesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Groups_GroupId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_GroupId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "GroupsClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsClasses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClasses_ClassId",
                table: "GroupsClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClasses_GroupId",
                table: "GroupsClasses",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsClasses");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GroupId",
                table: "Classes",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Groups_GroupId",
                table: "Classes",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
