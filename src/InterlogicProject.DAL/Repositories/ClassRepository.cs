using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class ClassRepository : BaseRepository<Class>
	{
		public ClassRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.Classes;
		}

		public override IQueryable<Class> GetAll()
			 => base.GetAll()
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Subject)
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Group)
							.ThenInclude(group => group.Curator)
								.ThenInclude(c => c.User)
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Group)
							.ThenInclude(group => group.Curator)
								.ThenInclude(c => c.Department)
									.ThenInclude(d => d.Faculty)
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Group)
							.ThenInclude(group => group.Students)
								.ThenInclude(s => s.User)
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Lecturer)
							.ThenInclude(lecturer => lecturer.User)
					.Include(c => c.GroupSubject)
						.ThenInclude(s => s.Lecturer)
							.ThenInclude(lecturer => lecturer.Department)
								.ThenInclude(d => d.Faculty);
	}
}
