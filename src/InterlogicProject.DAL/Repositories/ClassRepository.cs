using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassRepository : BaseRepository<Class>
	{
		public ClassRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Classes;
		}

		public override IQueryable<Class> GetAll()
			 => base.GetAll()
					.Include(c => c.Subject)
					.Include(c => c.Groups)
						.ThenInclude(g => g.Group)
					.Include(c => c.Places)
						.ThenInclude(p => p.Classroom)
							.ThenInclude(c => c.Building)
					.Include(c => c.Lecturers)
						.ThenInclude(l => l.Lecturer)
					.Include(c => c.Comments)
						.ThenInclude(c => c.User);
	}
}
