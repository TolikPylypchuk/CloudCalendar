using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerClassRepository : BaseRepository<LecturerClass>
	{
		public LecturerClassRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.LecturersClasses;
		}

		public override IQueryable<LecturerClass> GetAll()
			 => base.GetAll()
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Places)
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Lecturers)
					.Include(lc => lc.Lecturer)
						.ThenInclude(l => l.Department)
							.ThenInclude(d => d.Faculty)
					.Include(lc => lc.Lecturer)
						.ThenInclude(l => l.User);
	}
}
