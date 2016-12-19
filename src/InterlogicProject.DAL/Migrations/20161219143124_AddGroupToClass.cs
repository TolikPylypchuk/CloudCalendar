using Microsoft.EntityFrameworkCore.Migrations;

namespace InterlogicProject.DAL.Migrations
{
    public partial class AddGroupToClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
