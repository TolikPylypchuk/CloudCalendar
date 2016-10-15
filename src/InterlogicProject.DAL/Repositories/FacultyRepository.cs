using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class FacultyRepository : BaseRepository<Faculty>
	{
		public FacultyRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Faculties;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Faculty { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Faculty { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
