using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class CommentRepository : BaseRepository<Comment>
	{
		public CommentRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Comments;
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
