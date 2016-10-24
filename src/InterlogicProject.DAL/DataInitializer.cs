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

			await SeedDatabase(serviceProvider);
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

		private static async Task SeedDatabase(
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
					Email = "lecturer1@example.com",
					FirstName = "test1",
					MiddleName = "test1",
					LastName = "test1"
				},
				new User
				{
					Email = "lecturer2@example.com",
					FirstName = "test2",
					MiddleName = "test2",
					LastName = "test2"
				},
				new User
				{
					Email = "lecturer3@example.com",
					FirstName = "test3",
					MiddleName = "test3",
					LastName = "test3"
				},
				new User
				{
					Email = "student1@example.com",
					FirstName = "test4",
					MiddleName = "test4",
					LastName = "test4"
				},
				new User
				{
					Email = "student1@example.com",
					FirstName = "test5",
					MiddleName = "test5",
					LastName = "test5"
				}
			};
			
			await userManager.AddToRoleAsync(users[0], "Lecturer");
			await userManager.AddToRoleAsync(users[1], "Lecturer");
			await userManager.AddToRoleAsync(users[2], "Lecturer");
			await userManager.AddToRoleAsync(users[3], "Student");
			await userManager.AddToRoleAsync(users[4], "Student");

			for (int i = 0; i < 5; i++)
			{
				await userManager.CreateAsync(users[i], "secret");
			}

			var lecturers = new[]
			{
				new Lecturer
				{
					User = users[0],
					IsAdmin = true,
					IsDean = true,
					IsManager = false
				},
				new Lecturer
				{
					User = users[1],
					IsAdmin = false,
					IsDean = false,
					IsManager = true
				},
				new Lecturer
				{
					User = users[2],
					IsAdmin = false,
					IsManager = true
				},
			};

			var students = new[]
			{
				new Student
				{
					User = users[3],
					StudentNumber = "1",
					NumberInGroup = 1,
					IsGroupLeader = true
				},
				new Student
				{
					User = users[4],
					StudentNumber = "2",
					NumberInGroup = 2,
					IsGroupLeader = false
				}
			};

			var faculty = new Faculty
			{
				Name = "testFaculty",
			};

			var departments = new[]
			{
				new Department
				{
					Name = "testDepartment1",
					Faculty = faculty
				},
				new Department
				{
					Name = "testDepartment2",
					Faculty = faculty
				}
			};

			var group = new Group
			{
				Name = "testGroup",
				Year = 1,
				Curator = lecturers[1],
				Department = departments[0],
			};

			lecturers[0].Department = departments[0];
			lecturers[1].Department = departments[0];
			lecturers[2].Department = departments[1];

			students[0].Group = group;
			students[1].Group = group;

			context.Add(faculty);
			context.AddRange(departments);
			context.AddRange(lecturers);
			context.Add(group);
			context.AddRange(students);

			context.SaveChanges();
		}
	}
}
