using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerRepository : BaseRepository<Lecturer>
	{
		public LecturerRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Lecturers;
		}
	}
}
