using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data
{
	[ExcludeFromCodeCoverage]
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Building> Buildings { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<ClassPlace> ClassPlaces { get; set; }
		public DbSet<Classroom> Classrooms { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupClass> GroupsClasses { get; set; }
		public DbSet<Homework> Homeworks { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<LecturerClass> LecturersClasses { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Faculty>()
				   .HasIndex(f => f.Name)
				   .IsUnique();

			builder.Entity<Building>()
				   .HasIndex(b => b.Name)
				   .IsUnique();

			builder.Entity<Building>()
				   .HasIndex(b => b.Address)
				   .IsUnique();
			
			builder.Entity<Group>()
				   .HasIndex(g => g.Name)
				   .IsUnique();

			builder.Entity<Homework>()
				   .HasIndex(h => h.FileName)
				   .IsUnique();

			builder.Entity<Material>()
				   .HasIndex(m => m.FileName)
				   .IsUnique();

			builder.Entity<Student>()
				   .HasIndex(s => s.TranscriptNumber)
				   .IsUnique();

			builder.Entity<Subject>()
				   .HasIndex(s => s.Name)
				   .IsUnique();
		}
	}
}
