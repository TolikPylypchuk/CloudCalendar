using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class NotificationRepository : RepositoryBase<Notification>
	{
		public NotificationRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Notifications;
		}
	}
}
