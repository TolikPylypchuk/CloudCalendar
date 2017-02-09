using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class SubjectRepository : BaseRepository<Subject>
	{
		public SubjectRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Subjects;
		}
	}
}
