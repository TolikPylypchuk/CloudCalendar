using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL.Infrastructure
{
	public static class Extensions
	{
		public static TEntity Find<TEntity>(
			this DbSet<TEntity> set,
			params object[] keyValues)
			where TEntity : EntityBase
		{
			var context = set.GetService<DbContext>();

			var entityType = context.Model.FindEntityType(typeof(TEntity));
			var key = entityType.FindPrimaryKey();

			var entries = context.ChangeTracker.Entries<TEntity>();

			var i = 0;
			foreach (var property in key.Properties)
			{
				var i1 = i;
				entries = entries.Where(
					e => e.Property(property.Name).CurrentValue ==
						 keyValues[i1]);
				i++;
			}

			var entry = entries.FirstOrDefault();

			return entry != null
				? entry.Entity
				: set.FirstOrDefault(x => x.Id == (int)keyValues[0]);
		}
	}
}
