using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class FacultyRepository : BaseRepository<Faculty>
	{
		public FacultyRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Faculties;
		}
	}
}
