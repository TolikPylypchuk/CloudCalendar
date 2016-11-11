using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterlogicProject.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20161024151447_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InterlogicProject.DAL.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacultyId");

                    b.Property<int>("ManagerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeanId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("DeanId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CuratorId");

                    b.Property<int>("DepartmentId");

                    b.Property<int>("GroupLeaderId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("GroupLeaderId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentId");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<int>("NumberInGroup");

                    b.Property<string>("StudentNumber")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
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

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("InterlogicProject.DAL.Models.Department", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.Lecturer", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Faculty", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Lecturer", "Dean")
                        .WithMany()
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Group", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Lecturer", "Curator")
                        .WithMany()
                        .HasForeignKey("CuratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.Student", "GroupLeader")
                        .WithMany()
                        .HasForeignKey("GroupLeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Lecturer", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Department", "Department")
                        .WithOne()
                        .HasForeignKey("InterlogicProject.DAL.Models.Lecturer", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterlogicProject.DAL.Models.Student", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Group", "Group")
                        .WithOne()
                        .HasForeignKey("InterlogicProject.DAL.Models.Student", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.User", "User")
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
                    b.HasOne("InterlogicProject.DAL.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.User")
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

                    b.HasOne("InterlogicProject.DAL.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
