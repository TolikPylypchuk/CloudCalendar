using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class NotificationTextRepository : RepositoryBase<NotificationText>
	{
		public NotificationTextRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.NotificationTexts;
		}
	}
}
