using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassPlaceRepository : BaseRepository<ClassPlace>
	{
		public ClassPlaceRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.ClassPlaces;
		}

		public override IQueryable<ClassPlace> GetAll()
			 => base.GetAll()
					.Include(p => p.Classroom)
						.ThenInclude(c => c.Building)
							.ThenInclude(b => b.Faculties)
								.ThenInclude(f => f.Departments)
					.Include(p => p.Class)
						.ThenInclude(c => c.Subject)
					.Include(p => p.Class)
						.ThenInclude(c => c.Places);
	}
}
