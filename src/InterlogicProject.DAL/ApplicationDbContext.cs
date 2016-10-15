using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Group> Groups { get; set; }

		protected override void OnConfiguring(
			DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=InterlogicProject;Trusted_Connection=True;");
		}
	}
}
