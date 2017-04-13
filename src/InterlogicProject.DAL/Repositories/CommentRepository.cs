using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class CommentRepository : BaseRepository<Comment>
	{
		public CommentRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Comments;
		}
		
		public override Comment GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Class).Load();
			entry.Reference(c => c.User).Load();

			return result;
		}

		public override async Task<Comment> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Class).Load();
			entry.Reference(c => c.User).Load();

			return result;
		}

		public override IQueryable<Comment> GetAll()
			 => base.GetAll()
					.Include(c => c.User)
					.Include(c => c.Class)
						.ThenInclude(c => c.Places)
							.ThenInclude(p => p.Classroom)
								.ThenInclude(c => c.Building);
	}
}
