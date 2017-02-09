using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerClassRepository : BaseRepository<LecturerClass>
	{
		public LecturerClassRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.LecturersClasses;
		}

		public override LecturerClass GetById(int id)
		{
			var result = base.GetById(id);
			var entry = this.Context.Entry(result);

			entry.Reference(lc => lc.Lecturer).Load();
			entry.Reference(lc => lc.Class).Load();

			return result;
		}

		public override async Task<LecturerClass> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);
			var entry = this.Context.Entry(result);

			entry.Reference(lc => lc.Lecturer).Load();
			entry.Reference(lc => lc.Class).Load();

			return result;
		}

		public override IQueryable<LecturerClass> GetAll()
			 => base.GetAll()
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Places)
					.Include(lc => lc.Class)
						.ThenInclude(c => c.Lecturers)
					.Include(lc => lc.Lecturer)
						.ThenInclude(l => l.Department)
							.ThenInclude(d => d.Faculty)
								.ThenInclude(f => f.Building)
					.Include(lc => lc.Lecturer)
						.ThenInclude(l => l.User);
	}
}
