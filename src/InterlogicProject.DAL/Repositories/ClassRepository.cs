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
					.Include(c => c.Places)
					.Include(c => c.Lecturers)
						.ThenInclude(l => l.Lecturer);
	}
}
