using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.DAL
{
	public static class DataInitializer
	{
		public static async Task InitializeDatabaseAsync(
			IServiceProvider serviceProvider)
		{
			await CreateRolesAsync(
				serviceProvider,
				new[] { "Lecturer", "Student" });

			await SeedDatabaseAsync(serviceProvider);
		}

		private static async Task CreateRolesAsync(
			IServiceProvider serviceProvider,
			IEnumerable<string> roleNames)
		{
			var roleManager = serviceProvider
				.GetRequiredService<RoleManager<IdentityRole>>();
			
			foreach (var roleName in roleNames)
			{
				var role = await roleManager.FindByNameAsync(roleName);
				if (role == null)
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}
		}

		private static async Task SeedDatabaseAsync(
			IServiceProvider serviceProvider)
		{
			var context = serviceProvider
				.GetRequiredService<AppDbContext>();
			var userManager = serviceProvider
				.GetRequiredService<UserManager<User>>();
			
			if (userManager.Users.Any())
			{
				return;
			}

			context.SaveChanges();
		}
	}
}
