using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupClassRepository : BaseRepository<GroupClass>
	{
		public GroupClassRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.GroupsClasses;
		}

		public override IQueryable<GroupClass> GetAll()
			 => base.GetAll()
					.Include(gc => gc.Class)
						.ThenInclude(c => c.Places)
					.Include(gc => gc.Class)
						.ThenInclude(c => c.Lecturers)
							.ThenInclude(lc => lc.Lecturer)
								.ThenInclude(l => l.User)
					.Include(gc => gc.Group)
						.ThenInclude(g => g.Students)
							.ThenInclude(s => s.User);
	}
}
