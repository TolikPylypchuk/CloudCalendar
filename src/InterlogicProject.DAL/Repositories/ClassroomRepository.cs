using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassroomRepository : BaseRepository<Classroom>
	{
		public ClassroomRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Classrooms;
		}
		
		public override Classroom GetById(int id)
		{
			var result = base.GetById(id);
			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Building).Load();

			return result;
		}

		public override async Task<Classroom> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);
			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Building).Load();

			return result;
		}

		public override IQueryable<Classroom> GetAll()
			 => base.GetAll()
					.Include(c => c.Building)
						.ThenInclude(b => b.Faculties)
							.ThenInclude(f => f.Departments);
	}
}
