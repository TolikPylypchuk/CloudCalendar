using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassRepository : RepositoryBase<Class>
	{
		public ClassRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Classes;
		}

		public override Class GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Subject).Load();
			entry.Collection(c => c.Places).Load();
			entry.Collection(c => c.Groups).Load();
			entry.Collection(c => c.Lecturers).Load();
			entry.Collection(c => c.Comments).Load();

			return result;
		}

		public override async Task<Class> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(c => c.Subject).Load();
			entry.Collection(c => c.Places).Load();
			entry.Collection(c => c.Groups).Load();
			entry.Collection(c => c.Lecturers).Load();
			entry.Collection(c => c.Comments).Load();

			return result;
		}

		public override IQueryable<Class> GetAll()
			 => base.GetAll()
					.Include(c => c.Subject)
					.Include(c => c.Groups)
						.ThenInclude(g => g.Group)
					.Include(c => c.Places)
						.ThenInclude(p => p.Classroom)
							.ThenInclude(c => c.Building)
					.Include(c => c.Lecturers)
						.ThenInclude(l => l.Lecturer)
					.Include(c => c.Comments)
						.ThenInclude(c => c.User);
	}
}
