using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class LecturerRepository : RepositoryBase<Lecturer>
	{
		public LecturerRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Lecturers;
		}

		public override Lecturer GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(l => l.User).Load();
			entry.Reference(l => l.Department).Load();

			return result;
		}

		public override async Task<Lecturer> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(l => l.User).Load();
			entry.Reference(l => l.Department).Load();

			return result;
		}

		public override IQueryable<Lecturer> GetAll()
			 => base.GetAll()
					.Include(lecturer => lecturer.User)
					.Include(lecturer => lecturer.Department)
						.ThenInclude(d => d.Faculty)
					.Include(lecturer => lecturer.Department)
						.ThenInclude(d => d.Lecturers)
							.ThenInclude(lecturer => lecturer.User);
	}
}
