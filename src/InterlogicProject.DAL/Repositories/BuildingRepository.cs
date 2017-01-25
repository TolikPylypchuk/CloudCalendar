using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class BuildingRepository : BaseRepository<Building>
	{
		public BuildingRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Buildings;
		}

		public override IQueryable<Building> GetAll()
			 => base.GetAll()
					.Include(b => b.Faculties)
						.ThenInclude(f => f.Departments);
	}
}
