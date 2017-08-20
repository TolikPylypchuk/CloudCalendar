using System;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudCalendar.Data.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					FirstName = table.Column<string>(maxLength: 30, nullable: false),
					LastName = table.Column<string>(maxLength: 30, nullable: false),
					LockoutEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					MiddleName = table.Column<string>(maxLength: 30, nullable: false),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					PasswordHash = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					SecurityStamp = table.Column<string>(nullable: true),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					UserName = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<string>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true),
					RoleId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<string>(nullable: false),
					RoleId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Groups",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					CuratorId = table.Column<int>(nullable: false),
					DepartmentId = table.Column<int>(nullable: false),
					GroupLeaderId = table.Column<int>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Year = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Groups", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Students",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					GroupId = table.Column<int>(nullable: false),
					NumberInGroup = table.Column<int>(nullable: false),
					StudentNumber = table.Column<string>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Students", x => x.Id);
					table.ForeignKey(
						name: "FK_Students_Groups_GroupId",
						column: x => x.GroupId,
						principalTable: "Groups",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Students_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Lecturers",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					DepartmentId = table.Column<int>(nullable: false),
					IsAdmin = table.Column<bool>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Lecturers", x => x.Id);
					table.ForeignKey(
						name: "FK_Lecturers_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Faculties",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					DeanId = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Faculties", x => x.Id);
					table.ForeignKey(
						name: "FK_Faculties_Lecturers_DeanId",
						column: x => x.DeanId,
						principalTable: "Lecturers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					FacultyId = table.Column<int>(nullable: false),
					ManagerId = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Departments", x => x.Id);
					table.ForeignKey(
						name: "FK_Departments_Faculties_FacultyId",
						column: x => x.FacultyId,
						principalTable: "Faculties",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Departments_Lecturers_ManagerId",
						column: x => x.ManagerId,
						principalTable: "Lecturers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Departments_FacultyId",
				table: "Departments",
				column: "FacultyId");

			migrationBuilder.CreateIndex(
				name: "IX_Departments_ManagerId",
				table: "Departments",
				column: "ManagerId");

			migrationBuilder.CreateIndex(
				name: "IX_Faculties_DeanId",
				table: "Faculties",
				column: "DeanId");

			migrationBuilder.CreateIndex(
				name: "IX_Groups_CuratorId",
				table: "Groups",
				column: "CuratorId");

			migrationBuilder.CreateIndex(
				name: "IX_Groups_DepartmentId",
				table: "Groups",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Groups_GroupLeaderId",
				table: "Groups",
				column: "GroupLeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_Lecturers_DepartmentId",
				table: "Lecturers",
				column: "DepartmentId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Lecturers_UserId",
				table: "Lecturers",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Students_GroupId",
				table: "Students",
				column: "GroupId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Students_UserId",
				table: "Students",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_UserId",
				table: "AspNetUserRoles",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_Groups_Lecturers_CuratorId",
				table: "Groups",
				column: "CuratorId",
				principalTable: "Lecturers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Groups_Departments_DepartmentId",
				table: "Groups",
				column: "DepartmentId",
				principalTable: "Departments",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Groups_Students_GroupLeaderId",
				table: "Groups",
				column: "GroupLeaderId",
				principalTable: "Students",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Lecturers_Departments_DepartmentId",
				table: "Lecturers",
				column: "DepartmentId",
				principalTable: "Departments",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Departments_Faculties_FacultyId",
				table: "Departments");

			migrationBuilder.DropForeignKey(
				name: "FK_Departments_Lecturers_ManagerId",
				table: "Departments");

			migrationBuilder.DropForeignKey(
				name: "FK_Groups_Lecturers_CuratorId",
				table: "Groups");

			migrationBuilder.DropForeignKey(
				name: "FK_Groups_Departments_DepartmentId",
				table: "Groups");

			migrationBuilder.DropForeignKey(
				name: "FK_Groups_Students_GroupLeaderId",
				table: "Groups");

			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Faculties");

			migrationBuilder.DropTable(
				name: "Lecturers");

			migrationBuilder.DropTable(
				name: "Departments");

			migrationBuilder.DropTable(
				name: "Students");

			migrationBuilder.DropTable(
				name: "Groups");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
