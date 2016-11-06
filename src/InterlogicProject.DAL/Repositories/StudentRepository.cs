using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class StudentRepository : BaseRepository<Student>
	{
		public StudentRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Students;
		}

		public override IQueryable<Student> GetAll()
			=> base.GetAll()
				   .Include(s => s.User)
				   .Include(s => s.Group)
						.ThenInclude(group => group.Department)
							.ThenInclude(d => d.Faculty)
				   .Include(s => s.Group)
						.ThenInclude(group => group.Curator)
							.ThenInclude(curator => curator.User);
	}
}
