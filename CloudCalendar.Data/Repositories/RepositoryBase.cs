using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CloudCalendar.Data.Models;

namespace CloudCalendar.Data.Repositories
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase, new()
	{
		protected DbSet<TEntity> Table;

		protected RepositoryBase(AppDbContext context)
		{
			this.Context = context;
		}

		public AppDbContext Context { get; }

		public virtual int Add(TEntity entity)
		{
			this.Table.Add(entity);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddAsync(TEntity entity)
		{
			this.Table.Add(entity);
			return this.Context.SaveChangesAsync();
		}

		public virtual int AddRange(IEnumerable<TEntity> entities)
		{
			this.Table.AddRange(entities);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddRangeAsync(IEnumerable<TEntity> enitities)
		{
			this.Table.AddRange(enitities);
			return this.Context.SaveChangesAsync();
		}

		public virtual int Update(TEntity entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> UpdateAsync(TEntity entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChangesAsync();
		}

		public virtual int Delete(int id)
		{
			var entry = this.Context.ChangeTracker.Entries()
				.FirstOrDefault(e => (e.Entity as EntityBase)?.Id == id)
				?? this.Context.Entry(new TEntity { Id = id });

			entry.State = EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> DeleteAsync(int id)
		{
			var entry = this.Context.ChangeTracker.Entries()
				.FirstOrDefault(e => (e.Entity as EntityBase)?.Id == id)
				?? this.Context.Entry(new TEntity { Id = id });

			entry.State = EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}

		public virtual int Delete(TEntity entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public virtual Task<int> DeleteAsync(TEntity entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}

		public virtual TEntity GetById(int id)
			=> this.Table.Find(id);

		public virtual Task<TEntity> GetByIdAsync(int id)
			=> this.Table.FindAsync(id);

		public virtual IQueryable<TEntity> GetAll() => this.Table;
	}
}
