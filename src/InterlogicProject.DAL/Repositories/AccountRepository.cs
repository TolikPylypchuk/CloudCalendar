using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class AccountRepository : BaseRepository<Account>
	{
		public AccountRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Accounts;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Account { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Account { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
