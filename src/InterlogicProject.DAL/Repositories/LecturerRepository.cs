using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerRepository : BaseRepository<Lecturer>
	{
		public LecturerRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Lecturers;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Lecturer { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Lecturer { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
