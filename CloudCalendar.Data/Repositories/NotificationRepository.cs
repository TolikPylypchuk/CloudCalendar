using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
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
