using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterlogicProject.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Account",
				b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Department",
				b =>
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

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Faculty",
				b =>
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

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Group",
				b =>
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

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Lecturer",
				b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("DepartmentId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Student",
				b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("GroupId");

                    b.Property<int>("NumberInGroup");

                    b.Property<string>("StudentNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Department",
				b =>
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

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Faculty",
				b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Lecturer", "Dean")
                        .WithMany()
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Group",
				b =>
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

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Lecturer",
				b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.Department", "Department")
                        .WithOne()
                        .HasForeignKey("InterlogicProject.DAL.Models.Lecturer", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity(
				"InterlogicProject.DAL.Models.Student",
				b =>
                {
                    b.HasOne("InterlogicProject.DAL.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterlogicProject.DAL.Models.Group", "Group")
                        .WithOne()
                        .HasForeignKey("InterlogicProject.DAL.Models.Student", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
