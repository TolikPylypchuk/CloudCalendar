using System.Linq;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class GroupSubjectRepository : BaseRepository<GroupSubject>
	{
		public GroupSubjectRepository(AppDbContext context)
			: base(context)
		{
			this.table = this.Context.GroupSubjects;
		}

		public override IQueryable<GroupSubject> GetAll()
			 => base.GetAll()
					.Include(s => s.Subject)
					.Include(s => s.Group)
						.ThenInclude(group => group.Curator)
							.ThenInclude(curator => curator.User)
					.Include(s => s.Group)
						.ThenInclude(group => group.Curator)
							.ThenInclude(curator => curator.Department)
								.ThenInclude(d => d.Faculty)
					.Include(s => s.Group)
						.ThenInclude(group => group.Students)
							.ThenInclude(student => student.User)
					.Include(s => s.Lecturer)
						.ThenInclude(lecturer => lecturer.User)
					.Include(s => s.Lecturer)
						.ThenInclude(lecturer => lecturer.Department)
							.ThenInclude(d => d.Faculty);
	}
}
