using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(
			DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Group> Groups { get; set; }
	}
}
