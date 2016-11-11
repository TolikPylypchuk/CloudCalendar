using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupRepository : BaseRepository<Group>
	{
		public GroupRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Groups;
		}

		public override IQueryable<Group> GetAll()
			=> base.GetAll()
				   .Include(group => group.Curator)
						.ThenInclude(curator => curator.User)
				   .Include(group => group.Curator)
						.ThenInclude(curator => curator.Department)
							.ThenInclude(d => d.Faculty)
				   .Include(group => group.Students)
						.ThenInclude(student => student.User);
	}
}
