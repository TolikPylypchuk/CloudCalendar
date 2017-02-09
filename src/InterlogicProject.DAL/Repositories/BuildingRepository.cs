using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class BuildingRepository : BaseRepository<Building>
	{
		public BuildingRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Buildings;
		}

		public override Building GetById(int id)
		{
			var result = base.GetById(id);
			var entry = this.Context.Entry(result);

			entry.Collection(b => b.Faculties).Load();

			return result;
		}

		public override async Task<Building> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);
			var entry = this.Context.Entry(result);

			entry.Collection(b => b.Faculties).Load();

			return result;
		}

		public override IQueryable<Building> GetAll()
			 => base.GetAll()
					.Include(b => b.Faculties)
						.ThenInclude(f => f.Departments);
	}
}
