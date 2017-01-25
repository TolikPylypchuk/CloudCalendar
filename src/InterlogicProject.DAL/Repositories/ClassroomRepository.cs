using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassroomRepository : BaseRepository<Classroom>
	{
		public ClassroomRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Classrooms;
		}

		public override IQueryable<Classroom> GetAll()
			 => base.GetAll()
					.Include(c => c.Building)
						.ThenInclude(b => b.Faculties)
							.ThenInclude(f => f.Departments);
	}
}
