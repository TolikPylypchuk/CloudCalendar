using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterlogicProject.DAL.Repositories
{
	public interface IRepository<TEntity>
	{
		int Add(TEntity entity);
		Task<int> AddAsync(TEntity entity);

		int AddRange(IEnumerable<TEntity> entities);
		Task<int> AddRangeAsync(IEnumerable<TEntity> enitities);

		int Update(TEntity entity);
		Task<int> UpdateAsync(TEntity entity);

		int Delete(int id);
		Task<int> DeleteAsync(int id);

		int Delete(TEntity entity);
		Task<int> DeleteAsync(TEntity entity);

		TEntity GetById(int id);
		Task<TEntity> GetByIdAsync(int id);

		IQueryable<TEntity> GetAll();
	}
}
