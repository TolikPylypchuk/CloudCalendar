using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class DepartmentRepository : BaseRepository<Department>
	{
		public DepartmentRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Departments;
		}
	}
}
