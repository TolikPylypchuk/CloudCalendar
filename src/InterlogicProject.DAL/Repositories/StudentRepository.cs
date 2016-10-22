using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class StudentRepository : BaseRepository<Student>
	{
		public StudentRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Students;
		}
	}
}
