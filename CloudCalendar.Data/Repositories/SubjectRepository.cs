using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public class SubjectRepository : RepositoryBase<Subject>
	{
		public SubjectRepository(AppDbContext context)
			: base(context)
		{
			this.Table = this.Context.Subjects;
		}
	}
}
