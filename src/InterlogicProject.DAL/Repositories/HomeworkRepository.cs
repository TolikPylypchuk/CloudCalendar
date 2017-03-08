using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class HomeworkRepository : BaseRepository<Homework>
	{
		public HomeworkRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Homeworks;
		}

		public override Homework GetById(int id)
		{
			var result = base.GetById(id);
			var entry = this.Context.Entry(result);

			entry.Reference(h => h.Student).Load();
			entry.Reference(h => h.Class).Load();

			return result;
		}

		public override async Task<Homework> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);
			var entry = this.Context.Entry(result);

			entry.Reference(h => h.Student).Load();
			entry.Reference(h => h.Class).Load();

			return result;
		}

		public override IQueryable<Homework> GetAll()
			 => base.GetAll()
					.Include(h => h.Class)
						.ThenInclude(c => c.Groups)
							.ThenInclude(gc => gc.Group)
					.Include(h => h.Class)
						.ThenInclude(c => c.Lecturers)
							.ThenInclude(lc => lc.Lecturer)
								.ThenInclude(l => l.User)
					.Include(h => h.Student)
						.ThenInclude(s => s.User)
					.Include(h => h.Student)
						.ThenInclude(s => s.Group);
	}
}
