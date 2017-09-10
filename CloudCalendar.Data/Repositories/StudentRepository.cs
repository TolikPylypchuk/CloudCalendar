using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class StudentRepository : RepositoryBase<Student>
	{
		public StudentRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Students;
		}

		public override Student GetById(int id)
		{
			var result = base.GetById(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(d => d.User).Load();
			entry.Reference(d => d.Group).Load();

			return result;
		}

		public override async Task<Student> GetByIdAsync(int id)
		{
			var result = await base.GetByIdAsync(id);

			if (result == null)
			{
				return null;
			}

			var entry = this.Context.Entry(result);

			entry.Reference(d => d.User).Load();
			entry.Reference(d => d.Group).Load();

			return result;
		}

		public override IQueryable<Student> GetAll()
			 => base.GetAll()
					.Include(s => s.User)
					.Include(s => s.Group)
						.ThenInclude(group => group.Curator)
							.ThenInclude(curator => curator.User)
					.Include(s => s.Group)
						.ThenInclude(group => group.Curator)
							.ThenInclude(curator => curator.Department)
								.ThenInclude(d => d.Faculty)
					.Include(s => s.Group)
						.ThenInclude(group => group.Students)
							.ThenInclude(student => student.User);
	}
}
