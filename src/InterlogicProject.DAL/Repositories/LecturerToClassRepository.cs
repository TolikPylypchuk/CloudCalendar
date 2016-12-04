using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerToClassRepository : BaseRepository<LecturerToClass>
	{
		public LecturerToClassRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.LecturersToClasses;
		}

		public override IQueryable<LecturerToClass> GetAll()
			 => base.GetAll()
					.Include(lc => lc.Lecturer)
						.ThenInclude(lecturer => lecturer.User)
					.Include(lc => lc.Lecturer)
						.ThenInclude(lecturer => lecturer.Department)
							.ThenInclude(d => d.Faculty)
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Places)
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Subject);
	}
}
