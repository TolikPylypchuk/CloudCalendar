using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudCalendar.Data.Migrations
{
	public partial class Notifications : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "NotificationTexts",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Text = table.Column<string>(maxLength: 300, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NotificationTexts", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Notifications",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					IsSeen = table.Column<bool>(nullable: false),
					TextId = table.Column<int>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Notifications", x => x.Id);
					table.ForeignKey(
						name: "FK_Notifications_NotificationTexts_TextId",
						column: x => x.TextId,
						principalTable: "NotificationTexts",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Notifications_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_TextId",
				table: "Notifications",
				column: "TextId");

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_UserId",
				table: "Notifications",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Notifications");

			migrationBuilder.DropTable(
				name: "NotificationTexts");
		}
	}
}
