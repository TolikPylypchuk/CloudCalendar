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
					FirstName = "Іван",
					MiddleName = "Іванович",
					LastName = "Дияк"
				},
				new User
				{
					UserName = "shynkarenko.heorhiy@example.com",
					Email = "shynkarenko.heorhiy@example.com",
					NormalizedEmail = "SHYNKARENKO.HEORHIY@EXAMPLE.COM",
					FirstName = "Георгій",
					MiddleName = "Іванович",
					LastName = "Шинкаренко"
				},
				new User
				{
					UserName = "horlatch.vitaliy@example.com",
					Email = "horlatch.vitaliy@example.com",
					NormalizedEmail = "HORLATCH.VITALIY@EXAMPLE.COM",
					FirstName = "Віталій",
					MiddleName = "Михайлович",
					LastName = "Горлач"
				},
				new User
				{
					UserName = "vahin.petro@example.com",
					Email = "vahin.petro@example.com",
					NormalizedEmail = "VAHIN.PETRO@EXAMPLE.COM",
					FirstName = "Петро",
					MiddleName = "Петрович",
					LastName = "Вагін"
				},
				new User
				{
					UserName = "yaroshko.serhii@example.com",
					Email = "yaroshko.serhii@example.com",
					NormalizedEmail = "YAROSHKO.SERHII@EXAMPLE.COM",
					FirstName = "Сергій",
					MiddleName = "Адамович",
					LastName = "Ярошко"
				},
				new User
				{
					UserName = "kostiv.vasyl@example.com",
					Email = "kostiv.vasyl@example.com",
					NormalizedEmail = "KOSTIV.VASYL@EXAMPLE.COM",
					FirstName = "Василь",
					MiddleName = "Ярославович",
					LastName = "Костів"
				},
				new User
				{
					UserName = "hoshko.bogdan@example.com",
					Email = "hoshko.bogdan@example.com",
					NormalizedEmail = "HOSHKO.BOGDAN@EXAMPLE.COM",
					FirstName = "Богдан",
					MiddleName = "Мирославович",
					LastName = "Гошко"
				},
				new User
				{
					UserName = "malets.roma@example.com",
					Email = "malets.roma@example.com",
					NormalizedEmail = "MALETS.ROMA@EXAMPLE.COM",
					FirstName = "Рома",
					MiddleName = "Богданівна",
					LastName = "Малець"
				},
				new User
				{
					UserName = "pylypchuk.anatoliy@example.com",
					Email = "pylypchuk.anatoliy@example.com",
					NormalizedEmail = "PYLYPCHUK.ANATOLIY@EXAMPLE.COM",
					FirstName = "Анатолій",
					MiddleName = "Ігорович",
					LastName = "Пилипчук"
				},
				new User
				{
					UserName = "telepko.bozhena@example.com",
					Email = "telepko.bozhena@example.com",
					NormalizedEmail = "TELEPKO.BOZHENA@EXAMPLE.COM",
					FirstName = "Божена",
					MiddleName = "Тарасівна",
					LastName = "Телепко"
				},
				new User
				{
					UserName = "andrukhiv.ulyana@example.com",
					Email = "andrukhiv.ulyana@example.com",
					NormalizedEmail = "ANDRUKHIV.ULYANA@EXAMPLE.COM",
					FirstName = "Уляна",	
					MiddleName = "Тарасівна",
					LastName = "Андрухів"
				},
				new User
				{
					UserName = "zayats.artur@example.com",
					Email = "zayats.artur@example.com",
					NormalizedEmail = "ZAYATS.ARTUR@EXAMPLE.COM",
					FirstName = "Артур",
					MiddleName = "Романович",
					LastName = "Заяць"
				},
				new User
				{
					UserName = "slobodyaniuk.nataliia@example.com",
					Email = "slobodyaniuk.nataliia@example.com",
					NormalizedEmail = "SLOBODYIANIUK.NATALIIA@EXAMPLE.COM",
					FirstName = "Наталія",
					MiddleName = "Андріївна",
					LastName = "Слободянюк"
				},
				new User
				{
					UserName = "zvizlo.julia@example.com",
					Email = "zvizlo.julia@example.com",
					NormalizedEmail = "ZVIZLO.JULIA@EXAMPLE.COM",
					FirstName = "Юлія",
					MiddleName = "Зеновііївна",
					LastName = "Звізло"
				}
			};
			
			foreach (var user in users)
			{
				await userManager.CreateAsync(user, "secret");
			}

			for (int i = 0; i < 8; i++)
			{
				await userManager.AddToRoleAsync(users[i], "Lecturer");
			}

			for (int i = 8; i < 14; i++)
			{
				await userManager.AddToRoleAsync(users[i], "Student");
			}
			
			var faculty = new Faculty
			{
				Name = "Факультет прикладної математики та інформатики",
			};

			var departments = new[]
			{
				new Department
				{
					Name = "Кафедра програмування",
					Faculty = faculty
				},
				new Department
				{
					Name = "Кафедра інформаційних систем",
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
				},
				new Lecturer
				{
					User = users[7],
					IsAdmin = false,
					IsDean = false,
					IsHead = false,
					Department = departments[0]
				}
			};

			var group = new Group
			{
				Name = "ПМі-31",
				Year = 2014,
				Curator = lecturers[7]
			};

			var students = new[]
			{
				new Student
				{
					User = users[8],
					TranscriptNumber = "2714088с",
					Group = group,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[9],
					TranscriptNumber = "2714089с",
					Group = group,
					IsGroupLeader = true
				},
				new Student
				{
					User = users[10],
					TranscriptNumber = "2714090с",
					Group = group,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[11],
					TranscriptNumber = "2714091с",
					Group = group,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[12],
					TranscriptNumber = "2714092с",
					Group = group,
					IsGroupLeader = false
				},
				new Student
				{
					User = users[13],
					TranscriptNumber = "2714093с",
					Group = group,
					IsGroupLeader = false
				}
			};
			
			context.Add(faculty);
			context.AddRange(departments);
			context.AddRange(lecturers);
			context.Add(group);
			context.AddRange(students);

			context.SaveChanges();
		}
	}
}
