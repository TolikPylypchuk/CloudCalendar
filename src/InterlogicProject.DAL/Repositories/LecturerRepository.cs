using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class LecturerRepository : BaseRepository<Lecturer>
	{
		public LecturerRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Lecturers;
		}

		public override IQueryable<Lecturer> GetAll()
			=> base.GetAll()
				   .Include(lecturer => lecturer.User)
				   .Include(lecturer => lecturer.Department)
						.ThenInclude(d => d.Faculty);
	}
}
