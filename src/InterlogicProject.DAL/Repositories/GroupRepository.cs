using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupRepository : RepositoryBase<Group>
	{
		public GroupRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Groups;
		}

		public override Group GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(g => g.Curator).Load();
			entry.Collection(g => g.Students).Load();

			return result;
		}

		public override async Task<Group> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(g => g.Curator).Load();
			entry.Collection(g => g.Students).Load();

			return result;
		}

		public override IQueryable<Group> GetAll()
			 => base.GetAll()
					.Include(group => group.Curator)
						.ThenInclude(curator => curator.User)
					.Include(group => group.Curator)
						.ThenInclude(curator => curator.Department)
							.ThenInclude(d => d.Faculty)
								.ThenInclude(f => f.Building)
					.Include(group => group.Students)
						.ThenInclude(student => student.User);
	}
}
