using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupClassRepository : RepositoryBase<GroupClass>
	{
		public GroupClassRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.GroupsClasses;
		}

		public override GroupClass GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(gc => gc.Group).Load();
			entry.Reference(gc => gc.Class).Load();

			return result;
		}

		public override async Task<GroupClass> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(gc => gc.Group).Load();
			entry.Reference(gc => gc.Class).Load();

			return result;
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
