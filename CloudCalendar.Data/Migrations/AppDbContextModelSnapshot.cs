using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudCalendar.Data.Migrations
{
	[DbContext(typeof(AppDbContext))]
	partial class AppDbContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
			modelBuilder
				.HasAnnotation("ProductVersion", "1.1.2")
				.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			modelBuilder.Entity("CloudCalendar.Data.Models.Building", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("Address")
						.IsRequired()
						.HasMaxLength(30);

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(60);

					b.HasKey("Id");

					b.HasIndex("Address")
						.IsUnique();

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("Buildings");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Class", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<DateTime>("DateTime");

					b.Property<bool>("HomeworkEnabled");

					b.Property<int>("SubjectId");

					b.Property<string>("Type")
						.IsRequired()
						.HasMaxLength(15);

					b.HasKey("Id");

					b.HasIndex("SubjectId");

					b.ToTable("Classes");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.ClassPlace", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("ClassId");

					b.Property<int>("ClassroomId");

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("ClassroomId");

					b.ToTable("ClassPlaces");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Classroom", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("BuildingId");

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(10);

					b.HasKey("Id");

					b.HasIndex("BuildingId");

					b.ToTable("Classrooms");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Comment", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("ClassId");

					b.Property<DateTime>("DateTime");

					b.Property<string>("Text")
						.IsRequired()
						.HasMaxLength(300);

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("UserId");

					b.ToTable("Comments");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Department", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("FacultyId");

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(100);

					b.HasKey("Id");

					b.HasIndex("FacultyId");

					b.ToTable("Departments");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Faculty", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("BuildingId");

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(50);

					b.HasKey("Id");

					b.HasIndex("BuildingId");

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("Faculties");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Group", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("CuratorId");

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(10);

					b.Property<int>("Year");

					b.HasKey("Id");

					b.HasIndex("CuratorId");

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("Groups");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.GroupClass", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("ClassId");

					b.Property<int>("GroupId");

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("GroupId");

					b.ToTable("GroupsClasses");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Homework", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<bool?>("Accepted");

					b.Property<int>("ClassId");

					b.Property<DateTime>("DateTime");

					b.Property<string>("FileName")
						.IsRequired()
						.HasMaxLength(100);

					b.Property<int>("StudentId");

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("FileName")
						.IsUnique();

					b.HasIndex("StudentId");

					b.ToTable("Homeworks");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Lecturer", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("DepartmentId");

					b.Property<bool>("IsAdmin");

					b.Property<bool>("IsDean");

					b.Property<bool>("IsHead");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("DepartmentId");

					b.HasIndex("UserId");

					b.ToTable("Lecturers");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.LecturerClass", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("ClassId");

					b.Property<int>("LecturerId");

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("LecturerId");

					b.ToTable("LecturersClasses");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Material", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("ClassId");

					b.Property<string>("FileName")
						.IsRequired()
						.HasMaxLength(100);

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.HasIndex("FileName")
						.IsUnique();

					b.ToTable("Materials");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Notification", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int?>("ClassId");

					b.Property<DateTime>("DateTime");

					b.Property<string>("Text")
						.IsRequired()
						.HasMaxLength(300);

					b.HasKey("Id");

					b.HasIndex("ClassId");

					b.ToTable("Notifications");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Student", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("GroupId");

					b.Property<bool>("IsGroupLeader");

					b.Property<string>("TranscriptNumber")
						.IsRequired()
						.HasMaxLength(10);

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("GroupId");

					b.HasIndex("TranscriptNumber")
						.IsUnique();

					b.HasIndex("UserId");

					b.ToTable("Students");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Subject", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("Name")
						.IsRequired()
						.HasMaxLength(100);

					b.HasKey("Id");

					b.HasIndex("Name")
						.IsUnique();

					b.ToTable("Subjects");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.User", b =>
				{
					b.Property<string>("Id")
						.ValueGeneratedOnAdd();

					b.Property<int>("AccessFailedCount");

					b.Property<string>("ConcurrencyStamp")
						.IsConcurrencyToken();

					b.Property<string>("Email")
						.HasMaxLength(256);

					b.Property<bool>("EmailConfirmed");

					b.Property<string>("FirstName")
						.IsRequired()
						.HasMaxLength(30);

					b.Property<string>("LastName")
						.IsRequired()
						.HasMaxLength(30);

					b.Property<bool>("LockoutEnabled");

					b.Property<DateTimeOffset?>("LockoutEnd");

					b.Property<string>("MiddleName")
						.IsRequired()
						.HasMaxLength(30);

					b.Property<string>("NormalizedEmail")
						.HasMaxLength(256);

					b.Property<string>("NormalizedUserName")
						.HasMaxLength(256);

					b.Property<string>("PasswordHash");

					b.Property<string>("PhoneNumber");

					b.Property<bool>("PhoneNumberConfirmed");

					b.Property<string>("SecurityStamp");

					b.Property<bool>("TwoFactorEnabled");

					b.Property<string>("UserName")
						.HasMaxLength(256);

					b.HasKey("Id");

					b.HasIndex("NormalizedEmail")
						.HasName("EmailIndex");

					b.HasIndex("NormalizedUserName")
						.IsUnique()
						.HasName("UserNameIndex");

					b.ToTable("AspNetUsers");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.UserNotification", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<bool>("IsSeen");

					b.Property<int>("NotificationId");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("NotificationId");

					b.HasIndex("UserId");

					b.ToTable("UserNotifications");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
				{
					b.Property<string>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("ConcurrencyStamp")
						.IsConcurrencyToken();

					b.Property<string>("Name")
						.HasMaxLength(256);

					b.Property<string>("NormalizedName")
						.HasMaxLength(256);

					b.HasKey("Id");

					b.HasIndex("NormalizedName")
						.IsUnique()
						.HasName("RoleNameIndex");

					b.ToTable("AspNetRoles");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("ClaimType");

					b.Property<string>("ClaimValue");

					b.Property<string>("RoleId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("RoleId");

					b.ToTable("AspNetRoleClaims");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("ClaimType");

					b.Property<string>("ClaimValue");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("UserId");

					b.ToTable("AspNetUserClaims");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
				{
					b.Property<string>("LoginProvider");

					b.Property<string>("ProviderKey");

					b.Property<string>("ProviderDisplayName");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("LoginProvider", "ProviderKey");

					b.HasIndex("UserId");

					b.ToTable("AspNetUserLogins");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
				{
					b.Property<string>("UserId");

					b.Property<string>("RoleId");

					b.HasKey("UserId", "RoleId");

					b.HasIndex("RoleId");

					b.ToTable("AspNetUserRoles");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
				{
					b.Property<string>("UserId");

					b.Property<string>("LoginProvider");

					b.Property<string>("Name");

					b.Property<string>("Value");

					b.HasKey("UserId", "LoginProvider", "Name");

					b.ToTable("AspNetUserTokens");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Class", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Subject", "Subject")
						.WithMany()
						.HasForeignKey("SubjectId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.ClassPlace", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany("Places")
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.Classroom", "Classroom")
						.WithMany("Classes")
						.HasForeignKey("ClassroomId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Classroom", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Building", "Building")
						.WithMany()
						.HasForeignKey("BuildingId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Comment", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany("Comments")
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.User", "User")
						.WithMany()
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Department", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Faculty", "Faculty")
						.WithMany("Departments")
						.HasForeignKey("FacultyId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Faculty", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Building", "Building")
						.WithMany("Faculties")
						.HasForeignKey("BuildingId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Group", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Lecturer", "Curator")
						.WithMany()
						.HasForeignKey("CuratorId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.GroupClass", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany("Groups")
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.Group", "Group")
						.WithMany("Classes")
						.HasForeignKey("GroupId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Homework", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany()
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.Student", "Student")
						.WithMany()
						.HasForeignKey("StudentId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Lecturer", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Department", "Department")
						.WithMany("Lecturers")
						.HasForeignKey("DepartmentId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.User", "User")
						.WithMany()
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.LecturerClass", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany("Lecturers")
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.Lecturer", "Lecturer")
						.WithMany("Classes")
						.HasForeignKey("LecturerId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Material", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany()
						.HasForeignKey("ClassId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Notification", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Class", "Class")
						.WithMany()
						.HasForeignKey("ClassId");
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.Student", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Group", "Group")
						.WithMany("Students")
						.HasForeignKey("GroupId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.User", "User")
						.WithMany()
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("CloudCalendar.Data.Models.UserNotification", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.Notification", "Notification")
						.WithMany()
						.HasForeignKey("NotificationId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.User", "User")
						.WithMany()
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
				{
					b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
						.WithMany("Claims")
						.HasForeignKey("RoleId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.User")
						.WithMany("Claims")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
				{
					b.HasOne("CloudCalendar.Data.Models.User")
						.WithMany("Logins")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
				{
					b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
						.WithMany("Users")
						.HasForeignKey("RoleId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("CloudCalendar.Data.Models.User")
						.WithMany("Roles")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});
		}
	}
}
