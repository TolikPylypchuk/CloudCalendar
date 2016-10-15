using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupRepository : BaseRepository<Group>
	{
		public GroupRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Groups;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Group { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Group { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
