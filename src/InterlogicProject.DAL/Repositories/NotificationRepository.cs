using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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

		public override Notification GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(n => n.Text).Load();
			entry.Reference(n => n.User).Load();

			return result;
		}

		public override async Task<Notification> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(n => n.Text).Load();
			entry.Reference(n => n.User).Load();

			return result;
		}

		public override IQueryable<Notification> GetAll()
			 => base.GetAll()
					.Include(n => n.Text)
					.Include(n => n.User);
	}
}
