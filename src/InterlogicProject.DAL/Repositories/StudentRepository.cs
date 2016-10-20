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
	}
}
