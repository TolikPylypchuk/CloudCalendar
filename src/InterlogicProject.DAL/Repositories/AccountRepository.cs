using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class AccountRepository : BaseRepository<Account>
	{
		public AccountRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Accounts;
		}
	}
}
