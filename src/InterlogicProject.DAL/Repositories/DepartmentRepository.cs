using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public class DepartmentRepository : BaseRepository<Department>
	{
		public DepartmentRepository(ApplicationDbContext context)
			: base(context)
		{
			this.table = this.Context.Departments;
		}

		public override int Delete(int id)
		{
			this.Context.Entry(new Department { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new Department { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}
	}
}
