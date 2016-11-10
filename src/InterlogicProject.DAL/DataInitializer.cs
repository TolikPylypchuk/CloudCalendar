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
			
			var users = new[]
			{
				new User
				{
					UserName = "dyyak.ivan@example.com",
					Email = "dyyak.ivan@example.com",
					NormalizedEmail = "DYYAK.IVAN@EXAMPLE.COM",
					FirstName = "Ivan",
					MiddleName = "Ivanovych",
					LastName = "Dyyak"
				},
				new User
				{
					UserName = "shynkarenko.heorhiy@example.com",
					Email = "shynkarenko.heorhiy@example.com",
					NormalizedEmail = "SHYNKARENKO.HEORHIY@EXAMPLE.COM",
					FirstName = "Heorhiy",
					MiddleName = "Ivanovych",
					LastName = "Shynkarenko"
				},
				new User
				{
					UserName = "horlatch.vitaliy@example.com",
					Email = "horlatch.vitaliy@example.com",
					NormalizedEmail = "HORLATCH.VITALIY@EXAMPLE.COM",
					FirstName = "Vitaliy",
					MiddleName = "Mykhaylovych",
					LastName = "Horlatch"
				},
				new User
				{
					UserName = "vahin.petro@example.com",
					Email = "vahin.petro@example.com",
					NormalizedEmail = "VAHIN.PETRO@EXAMPLE.COM",
					FirstName = "Petro",
					MiddleName = "Petrovych",
					LastName = "Vahin"
				},
				new User
				{
					UserName = "yaroshko.serhii@example.com",
					Email = "yaroshko.serhii@example.com",
					NormalizedEmail = "YAROSHKO.SERHII@EXAMPLE.COM",
					FirstName = "Serhii",
					MiddleName = "Adamovych",
					LastName = "Yaroshko"
				},
				new User
				{
					UserName = "kostiv.vasyl@example.com",
					Email = "kostiv.vasyl@example.com",
					NormalizedEmail = "KOSTIV.VASYL@EXAMPLE.COM",
					FirstName = "Vasyl",
					MiddleName = "Yaroslavovych",
					LastName = "Kostiv"
				},
				new User
				{
					UserName = "hoshko.bogdan@example.com",
					Email = "hoshko.bogdan@example.com",
					NormalizedEmail = "HOSHKO.BOGDAN@EXAMPLE.COM",
					FirstName = "Bogdan",
					MiddleName = "Myroslavovych",
					LastName = "Hoshko"
				},
				new User
				{
					UserName = "pylypchuk.anatoliy@example.com",
					Email = "pylypchuk.anatoliy@example.com",
					NormalizedEmail = "PYLYPCHUK.ANATOLIY@EXAMPLE.COM",
					FirstName = "Anatoliy",
					MiddleName = "Ihorovych",
					LastName = "Pylypchuk"
				},
				new User
				{
					UserName = "telepko.bozhena@example.com",
					Email = "telepko.bozhena@example.com",
					NormalizedEmail = "TELEPKO.BOZHENA@EXAMPLE.COM",
					FirstName = "Bozhena",
					MiddleName = "Tarasivna",
					LastName = "Telepko"
				},
				new User
				{
					UserName = "andrukhiv.ulyana@example.com",
					Email = "andrukhiv.ulyana@example.com",
					NormalizedEmail = "ANDRUKHIV.ULYANA@EXAMPLE.COM",
					FirstName = "Ulyana",	
					MiddleName = "Tarasivna",
					LastName = "Andrukhiv"
				},
				new User
				{
					UserName = "zayats.artur@example.com",
					Email = "zayats.artur@example.com",
					NormalizedEmail = "ZAYATS.ARTUR@EXAMPLE.COM",
					FirstName = "Artur",
					MiddleName = "Romanovych",
					LastName = "Zayats"
				},
				new User
				{
					UserName = "slobodyaniuk.nataliia@example.com",
					Email = "slobodyaniuk.nataliia@example.com",
					NormalizedEmail = "SLOBODYIANIUK.NATALIIA@EXAMPLE.COM",
					FirstName = "Nataliia",
					MiddleName = "Andriivna",
					LastName = "Slobodyaniuk"
				},
				new User
				{
					UserName = "zvizlo.julia@example.com",
					Email = "zvizlo.julia@example.com",
					NormalizedEmail = "ZVIZLO.JULIA@EXAMPLE.COM",
					FirstName = "Julia",
					MiddleName = "Zenoviivna",
					LastName = "Zvizlo"
				}
			};
			
			foreach (var user in users)
			{
				await userManager.CreateAsync(user, "secret");
			}

			for (int i = 0; i < 7; i++)
			{
				await userManager.AddToRoleAsync(users[i], "Lecturer");
			}

			for (int i = 7; i < 13; i++)
			{
				await userManager.AddToRoleAsync(users[i], "Student");
			}
			
			var faculty = new Faculty
			{
				Name = "Faculty of Applied Mathematics and Informatics",
			};

			var departments = new[]
			{
				new Department
				{
					Name = "Department of Programming",
					Faculty = faculty
				},
				new Department
				{
					Name = "Department of Information Systems",
					Faculty = faculty
				}
			};

			var lecturers = new[]
			{
				new Lecturer
				{
					User = users[0],
					IsAdmin = true,
					IsDean = true,
					IsHead = false,
					Department = departments[1]
				},
				new Lecturer
				{
					User = users[1],
					IsAdmin = false,
					IsDean = false,
					IsHead = true,
					Department = departments[1]
				},
				new Lecturer
				{
					User = users[2],
					IsAdmin = true,
					IsDean = false,
					IsHead = false,
					Department = departments[1]
				},
				new Lecturer
				{
					User = users[3],
					IsAdmin = false,
					IsDean = false,
					IsHead = false,
					Department = departments[1]
				},
				new Lecturer
				{
					User = users[4],
					IsAdmin = false,
					IsDean = false,
					IsHead = true,
					Department = departments[0]
				},
				new Lecturer
				{
					User = users[5],
					IsAdmin = false,
					IsDean = false,
					IsHead = false,
					Department = departments[0]
				},
				new Lecturer
				{
					User = users[6],
					IsAdmin = false,
					IsDean = false,
					IsHead = false,
					Department = departments[0]
				}
			};

			var group = new Group
			{
				Name = "AMi-31",
				Year = 3,
				Curator = lecturers[5],
				Department = departments[0]
			};

			var students = new[]
			{
				new Student
				{
					User = users[7],
					StudentNumber = "16479s",
					Group = group,
					NumberInGroup = 16,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[8],
					StudentNumber = "16480s",
					Group = group,
					NumberInGroup = 21,
					IsGroupLeader = true
				},
				new Student
				{
					User = users[9],
					StudentNumber = "16478s",
					Group = group,
					NumberInGroup = 1,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[10],
					StudentNumber = "16481s",
					Group = group,
					NumberInGroup = 5,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[11],
					StudentNumber = "16482s",
					Group = group,
					NumberInGroup = 19,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[12],
					StudentNumber = "16482s",
					Group = group,
					NumberInGroup = 6,
					IsGroupLeader = false
				}
			};
			
			context.Add(faculty);
			context.AddRange(departments);
			context.AddRange(lecturers);
			context.Add(group);

			context.SaveChanges();

			context.AddRange(students);

			context.SaveChanges();
		}
	}
}
