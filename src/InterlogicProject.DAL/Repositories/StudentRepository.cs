using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class StudentRepository : BaseRepository<Student>
	{
		public StudentRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Students;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Student { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Student { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
