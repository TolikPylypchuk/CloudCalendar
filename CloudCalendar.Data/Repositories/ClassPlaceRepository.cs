using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class ClassPlaceRepository : RepositoryBase<ClassPlace>
	{
		public ClassPlaceRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.ClassPlaces;
		}

		public override ClassPlace GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(p => p.Class).Load();
			entry.Reference(p => p.Classroom).Load();

			return result;
		}

		public override async Task<ClassPlace> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(p => p.Class).Load();
			entry.Reference(p => p.Classroom).Load();

			return result;
		}

		public override IQueryable<ClassPlace> GetAll()
			 => base.GetAll()
					.Include(p => p.Classroom)
						.ThenInclude(c => c.Building)
							.ThenInclude(b => b.Faculties)
								.ThenInclude(f => f.Departments)
					.Include(p => p.Class)
						.ThenInclude(c => c.Subject)
					.Include(p => p.Class)
						.ThenInclude(c => c.Places);
	}
}
