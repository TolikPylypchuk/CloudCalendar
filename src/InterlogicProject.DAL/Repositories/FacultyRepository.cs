using System.Linq;

using Microsoft.EntityFrameworkCore;

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

		public override IQueryable<Faculty> GetAll()
			 => base.GetAll()
					.Include(f => f.Departments)
						.ThenInclude(d => d.Lecturers)
							.ThenInclude(lecturer => lecturer.User);
	}
}
