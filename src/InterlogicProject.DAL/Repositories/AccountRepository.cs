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
	}
}
