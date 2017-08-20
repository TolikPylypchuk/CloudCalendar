using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class UserNotificationRepository : RepositoryBase<UserNotification>
	{
		public UserNotificationRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.UserNotifications;
		}

		public override UserNotification GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(n => n.Notification).Load();
			entry.Reference(n => n.User).Load();

			return result;
		}

		public override async Task<UserNotification> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(n => n.Notification).Load();
			entry.Reference(n => n.User).Load();

			return result;
		}

		public override IQueryable<UserNotification> GetAll()
			 => base.GetAll()
					.Include(n => n.Notification)
					.Include(n => n.User);
	}
}
