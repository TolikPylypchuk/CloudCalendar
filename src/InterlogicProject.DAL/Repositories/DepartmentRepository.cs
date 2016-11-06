using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class DepartmentRepository : BaseRepository<Department>
	{
		public DepartmentRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Departments;
		}

		public override IQueryable<Department> GetAll()
			=> base.GetAll()
				   .Include(d => d.Faculty)
				   .Include(d => d.Lecturers)
						.ThenInclude(lecturer => lecturer.User);
	}
}
