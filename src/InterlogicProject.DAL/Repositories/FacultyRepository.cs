using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class FacultyRepository : RepositoryBase<Faculty>
	{
		public FacultyRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Faculties;
		}

		public override Faculty GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(f => f.Building).Load();
			entry.Collection(f => f.Departments).Load();

			return result;
		}

		public override async Task<Faculty> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(f => f.Building).Load();
			entry.Collection(f => f.Departments).Load();

			return result;
		}

		public override IQueryable<Faculty> GetAll()
			 => base.GetAll()
					.Include(f => f.Departments)
						.ThenInclude(d => d.Lecturers)
							.ThenInclude(lecturer => lecturer.User)
					.Include(f => f.Building)
						.ThenInclude(b => b.Faculties)
							.ThenInclude(f => f.Departments);
	}
}
