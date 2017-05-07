using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterlogicProject.DAL.Migrations
{
	public partial class NotificationMoveDateTime : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Notifications_NotificationTexts_TextId",
				table: "Notifications");

			migrationBuilder.DropForeignKey(
				name: "FK_Notifications_AspNetUsers_UserId",
				table: "Notifications");

			migrationBuilder.DropTable(
				name: "NotificationTexts");

			migrationBuilder.DropIndex(
				name: "IX_Notifications_TextId",
				table: "Notifications");

			migrationBuilder.DropIndex(
				name: "IX_Notifications_UserId",
				table: "Notifications");

			migrationBuilder.DropColumn(
				name: "IsSeen",
				table: "Notifications");

			migrationBuilder.DropColumn(
				name: "TextId",
				table: "Notifications");

			migrationBuilder.DropColumn(
				name: "UserId",
				table: "Notifications");

			migrationBuilder.AddColumn<string>(
				name: "Text",
				table: "Notifications",
				maxLength: 300,
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "UserNotifications",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					IsSeen = table.Column<bool>(nullable: false),
					NotificationId = table.Column<int>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserNotifications", x => x.Id);
					table.ForeignKey(
						name: "FK_UserNotifications_Notifications_NotificationId",
						column: x => x.NotificationId,
						principalTable: "Notifications",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserNotifications_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_UserNotifications_NotificationId",
				table: "UserNotifications",
				column: "NotificationId");

			migrationBuilder.CreateIndex(
				name: "IX_UserNotifications_UserId",
				table: "UserNotifications",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserNotifications");

			migrationBuilder.DropColumn(
				name: "Text",
				table: "Notifications");

			migrationBuilder.AddColumn<bool>(
				name: "IsSeen",
				table: "Notifications",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<int>(
				name: "TextId",
				table: "Notifications",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<string>(
				name: "UserId",
				table: "Notifications",
				nullable: false,
				defaultValue: "");

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

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_TextId",
				table: "Notifications",
				column: "TextId");

			migrationBuilder.CreateIndex(
				name: "IX_Notifications_UserId",
				table: "Notifications",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_Notifications_NotificationTexts_TextId",
				table: "Notifications",
				column: "TextId",
				principalTable: "NotificationTexts",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Notifications_AspNetUsers_UserId",
				table: "Notifications",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
