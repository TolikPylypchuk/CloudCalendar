using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Infrastructure;

namespace InterlogicProject.DAL.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T>
		where T : EntityBase, new()
	{
		protected DbSet<T> table;

		protected BaseRepository(AppDbContext context)
		{
			this.Context = context;
		}

		public AppDbContext Context { get; }

		public virtual int Add(T entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddAsync(T entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChangesAsync();
		}

		public virtual int AddRange(IEnumerable<T> entities)
		{
			this.table.AddRange(entities);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddRangeAsync(IEnumerable<T> enitities)
		{
			this.table.AddRange(enitities);
			return this.Context.SaveChangesAsync();
		}

		public virtual int Update(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> UpdateAsync(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChangesAsync();
		}

		public virtual int Delete(int id)
		{
			this.Context.Entry(new T { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> DeleteAsync(int id)
		{
			this.Context.Entry(new T { Id = id }).State =
				EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}

		public virtual int Delete(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> DeleteAsync(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}

		public virtual T GetById(int id) => this.table.Find(id);

		public Task<T> GetByIdAsync(int id)
		{
			return Task.Factory.StartNew(() => this.table.Find(id));
		}

		public IQueryable<T> GetAll() => this.table;
	}
}
