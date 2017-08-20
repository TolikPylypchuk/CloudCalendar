using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class MaterialRepository : RepositoryBase<Material>
	{
		public MaterialRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Materials;
		}

		public override Material GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(m => m.Class).Load();

			return result;
		}

		public override async Task<Material> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(m => m.Class).Load();

			return result;
		}

		public override IQueryable<Material> GetAll()
			 => base.GetAll()
					.Include(m => m.Class)
						.ThenInclude(c => c.Groups)
							.ThenInclude(gc => gc.Group)
					.Include(m => m.Class)
						.ThenInclude(c => c.Lecturers)
							.ThenInclude(lc => lc.Lecturer)
								.ThenInclude(l => l.User);
	}
}
