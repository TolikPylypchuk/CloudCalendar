using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Classroom> Classrooms { get; set; }
		public DbSet<ClassPlace> ClassPlaces { get; set; }
		public DbSet<LecturerClass> LecturersClasses { get; set; }
		public DbSet<Comment> Comments { get; set; }

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

			builder.Entity<Student>()
				   .HasIndex(s => s.TranscriptNumber)
				   .IsUnique();

			builder.Entity<Subject>()
				   .HasIndex(s => s.Name)
				   .IsUnique();
		}
	}
}
