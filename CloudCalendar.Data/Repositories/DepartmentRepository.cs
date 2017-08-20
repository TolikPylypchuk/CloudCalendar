using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class DepartmentRepository : RepositoryBase<Department>
	{
		public DepartmentRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Departments;
		}
		
		public override Department GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(d => d.Faculty).Load();
			entry.Collection(d => d.Lecturers).Load();

			return result;
		}

		public override async Task<Department> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(d => d.Faculty).Load();
			entry.Collection(d => d.Lecturers).Load();

			return result;
		}

		public override IQueryable<Department> GetAll()
			 => base.GetAll()
					.Include(d => d.Faculty)
						.ThenInclude(f => f.Building)
							.ThenInclude(b => b.Faculties)
								.ThenInclude(f => f.Departments)
					.Include(d => d.Lecturers)
						.ThenInclude(lecturer => lecturer.User);
	}
}
