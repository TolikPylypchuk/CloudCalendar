using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Repositories
{
	public abstract class BaseRepository<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase, new()
	{
		protected DbSet<TEntity> table;

		protected BaseRepository(AppDbContext context)
		{
			this.Context = context;
		}

		public AppDbContext Context { get; }

		public virtual int Add(TEntity entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddAsync(TEntity entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChangesAsync();
		}

		public virtual int AddRange(IEnumerable<TEntity> entities)
		{
			this.table.AddRange(entities);
			return this.Context.SaveChanges();
		}

		public virtual Task<int> AddRangeAsync(IEnumerable<TEntity> enitities)
		{
			this.table.AddRange(enitities);
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
			=> this.GetAll().FirstOrDefault(e => e.Id == id);

		public virtual Task<TEntity> GetByIdAsync(int id)
			=> Task.Factory.StartNew(
				() => this.GetAll().FirstOrDefault(e => e.Id == id));

		public virtual IQueryable<TEntity> GetAll() => this.table;
	}
}
