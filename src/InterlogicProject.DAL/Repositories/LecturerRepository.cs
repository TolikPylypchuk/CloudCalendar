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
	}
}
