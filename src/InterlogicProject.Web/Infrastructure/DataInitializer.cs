using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;

namespace InterlogicProject.Web.Infrastructure
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

			var buildings = ReadBuildings();
			var classrooms = ReadClassrooms(buildings);
			var faculties = ReadFaculties(buildings);
			var departments = ReadDepartments(faculties);
			var subjects = ReadSubjects();
			var users = ReadUsers();
			var lecturers = ReadLecturers(departments, users);
			var groups = ReadGroups(lecturers);
			var students = ReadStudents(groups, users);
			var classes = ReadClasses(subjects, out int classesInWeek);
			var places = ReadClassPlaces(classes, classrooms, classesInWeek);
			var lecturersClasses = ReadLecturersClasses(
				lecturers, classes, classesInWeek);
			var groupsClasses = ReadGroupsClasses(
				groups, classes, classesInWeek);

			foreach (var user in users)
			{
				user.Id = null;
				await userManager.CreateAsync(
					user, Program.DefaultPassword);
				await userManager.AddToRoleAsync(
					user,
					lecturers.Any(l => l.User.Email == user.Email)
						? "Lecturer"
						: "Student");
			}
			
			var collections = new IEnumerable<EntityBase>[]
			{
				buildings,
				classrooms,
				faculties,
				departments,
				subjects,
				lecturers,
				groups,
				students,
				classes,
				places,
				lecturersClasses,
				groupsClasses
			};

			foreach (var collection in collections)
			{
				foreach (var entity in collection)
				{
					entity.Id = 0;
				}
			}

			context.AddRange(buildings);
			context.AddRange(classrooms);
			context.AddRange(faculties);
			context.AddRange(departments);
			context.AddRange(subjects);
			context.AddRange(lecturers);
			context.AddRange(groups);
			context.AddRange(students);
			context.AddRange(classes);
			context.AddRange(places);
			context.AddRange(lecturersClasses);
			context.AddRange(groupsClasses);

			context.SaveChanges();
		}

		private static IList<Building> ReadBuildings()
		{
			string data = null;
			
			using (var reader = File.OpenText(@"Data\Buildings.txt"))
			{
				data = reader.ReadToEnd();
			}

			var buildingsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return buildingsData.Select(
				buildingData => buildingData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Building
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1],
					Address = tokens[2]
				}).ToList();
		}

		private static IList<Classroom> ReadClassrooms(
			IEnumerable<Building> buildings)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Classrooms.txt"))
			{
				data = reader.ReadToEnd();
			}

			var classroomsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return classroomsData.Select(
				classroomData => classroomData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Classroom
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1],
					Building = buildings.First(
						b => b.Id.ToString() == tokens[2])
				}).ToList();
		}

		private static IList<Faculty> ReadFaculties(
			IEnumerable<Building> buildings)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Faculties.txt"))
			{
				data = reader.ReadToEnd();
			}

			var facultiesData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return facultiesData.Select(
				facultyData => facultyData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Faculty
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1],
					Building = buildings.First(
						b => b.Id.ToString() == tokens[2])
				}).ToList();
		}

		private static IList<Department> ReadDepartments(
			IEnumerable<Faculty> faculties)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Departments.txt"))

			{
				data = reader.ReadToEnd();
			}

			var departmentsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return departmentsData.Select(
				departmentData => departmentData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Department
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1],
					Faculty = faculties.First(
						f => f.Id.ToString() == tokens[2])
				}).ToList();
		}

		private static IList<Subject> ReadSubjects()
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Subjects.txt"))
			{
				data = reader.ReadToEnd();
			}

			var subjectsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return subjectsData.Select(
				subjectData => subjectData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Subject
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1]
				}).ToList();
		}

		private static IList<User> ReadUsers()
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Users.txt"))
			{
				data = reader.ReadToEnd();
			}

			var usersData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return usersData.Select(
				userData => userData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens =>
				{
					var names = tokens[1].Split(' ');
					var email = tokens[2] + $"@{Program.EmailDomain}";

					return new User
					{
						Id = tokens[0],
						LastName = names[0],
						FirstName = names[1],
						MiddleName = names[2],
						Email = email,
						UserName = email,
						NormalizedEmail = email.ToUpper(),
						NormalizedUserName = email.ToUpper()
					};
				}).ToList();
		}

		private static IList<Lecturer> ReadLecturers(
			IEnumerable<Department> departments,
			IEnumerable<User> users)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Lecturers.txt"))
			{
				data = reader.ReadToEnd();
			}

			var lecturersData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return lecturersData.Select(
				lecturerData => lecturerData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Lecturer
				{
					Id = Int32.Parse(tokens[0]),
					User = users.First(
						u => u.Id == tokens[1]),
					Department = departments.First(
						d => d.Id.ToString() == tokens[2]),
					IsHead = Boolean.Parse(tokens[3]),
					IsDean = Boolean.Parse(tokens[4]),
					IsAdmin = Boolean.Parse(tokens[5])
				}).ToList();
		}

		private static IList<Group> ReadGroups(
			IEnumerable<Lecturer> lecturers)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Groups.txt"))
			{
				data = reader.ReadToEnd();
			}

			var groupsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return groupsData.Select(
				groupData => groupData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Group
				{
					Id = Int32.Parse(tokens[0]),
					Name = tokens[1],
					Year = Int32.Parse(tokens[2]),
					Curator = lecturers.First(
						l => l.Id.ToString() == tokens[3])
				}).ToList();
		}

		private static IList<Student> ReadStudents(
			IEnumerable<Group> groups,
			IEnumerable<User> users)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Students.txt"))
			{
				data = reader.ReadToEnd();
			}

			var studentsData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			return studentsData.Select(
				studentData => studentData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new Student
				{
					Id = Int32.Parse(tokens[0]),
					User = users.First(
						u => u.Id == tokens[1]),
					TranscriptNumber = tokens[2],
					Group = groups.First(
						g => g.Id.ToString() == tokens[3]),
					IsGroupLeader = Boolean.Parse(tokens[4])
				}).ToList();
		}

		private static IList<Class> ReadClasses(
			IEnumerable<Subject> subjects,
			out int classesInWeek)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\Classes.txt"))
			{
				data = reader.ReadToEnd();
			}

			var classesData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			var tempClasses = classesData.Select(
				classData => classData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new
				{
					Id = Int32.Parse(tokens[0]),
					Subject = subjects.First(
						s => s.Id.ToString() == tokens[1]),
					DateTime = DateTime.Parse(tokens[2]),
					Type = tokens[3],
					Repeat = Int32.Parse(tokens[4])
				}).ToList();

			var result = new List<Class>();

			classesInWeek = tempClasses.Count;

			foreach (var item in tempClasses)
			{
				var end = new DateTime(2017, 6, 1);

				for ((TimeSpan span, int i) = (TimeSpan.Zero, 0);
					 item.DateTime + span < end;
					 span += TimeSpan.FromDays(7 * item.Repeat), i++)
				{
					result.Add(new Class
					{
						Id = item.Id + classesInWeek * i,
						DateTime = item.DateTime + span,
						Subject = item.Subject,
						Type = item.Type,
						HomeworkEnabled = false
					});
				}
			}

			return result;
		}

		private static IList<ClassPlace> ReadClassPlaces(
			IEnumerable<Class> classes,
			IEnumerable<Classroom> classrooms,
			int classesInWeek)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\ClassPlaces.txt"))
			{
				data = reader.ReadToEnd();
			}

			var classPlacesData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			var tempClassPlaces = classPlacesData.Select(
				classPlaceData => classPlaceData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new ClassPlace
				{
					Id = Int32.Parse(tokens[0]),
					Class = classes.First(
						c => c.Id.ToString() == tokens[1]),
					Classroom = classrooms.First(
						c => c.Id.ToString() == tokens[2])
				}).ToList();

			var result = new List<ClassPlace>();

			var classesList = classes.ToList();

			foreach (var item in tempClassPlaces)
			{
				for (int id = item.Class.Id;
					 classesList.Any(c => c.Id == id);
					 id += classesInWeek)
				{
					result.Add(new ClassPlace
					{
						Class = classesList.First(c => c.Id == id),
						Classroom = item.Classroom
					});
				}
			}
			
			return result;
		}

		private static IList<LecturerClass> ReadLecturersClasses(
			IEnumerable<Lecturer> lecturers,
			IEnumerable<Class> classes,
			int classesInWeek)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\LecturersClasses.txt"))
			{
				data = reader.ReadToEnd();
			}

			var lecturersClassesData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			var tempLecturersClasses = lecturersClassesData.Select(
				lecturerClassData => lecturerClassData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new LecturerClass
				{
					Id = Int32.Parse(tokens[0]),
					Lecturer = lecturers.First(
						l => l.Id.ToString() == tokens[1]),
					Class = classes.First(
						c => c.Id.ToString() == tokens[2])
				}).ToList();

			var result = new List<LecturerClass>();

			var classesList = classes.ToList();

			foreach (var item in tempLecturersClasses)
			{
				for (int id = item.Class.Id;
					 classesList.Any(c => c.Id == id);
					 id += classesInWeek)
				{
					result.Add(new LecturerClass
					{
						Lecturer = item.Lecturer,
						Class = classesList.First(c => c.Id == id)
					});
				}
			}

			return result;
		}

		private static IList<GroupClass> ReadGroupsClasses(
			IEnumerable<Group> groups,
			IEnumerable<Class> classes,
			int classesInWeek)
		{
			string data = null;

			using (var reader = File.OpenText(@"Data\GroupsClasses.txt"))
			{
				data = reader.ReadToEnd();
			}

			var groupsClassesData = data.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);

			var tempGroupsClasses = groupsClassesData.Select(
				groupClassData => groupClassData.Split(
					new[] { '\t' },
					StringSplitOptions.RemoveEmptyEntries))
				.Select(tokens => new GroupClass
				{
					Id = Int32.Parse(tokens[0]),
					Group = groups.First(
						g => g.Id.ToString() == tokens[1]),
					Class = classes.First(
						c => c.Id.ToString() == tokens[2])
				}).ToList();

			var result = new List<GroupClass>();

			var classesList = classes.ToList();

			foreach (var item in tempGroupsClasses)
			{
				for (int id = item.Class.Id;
					 classesList.Any(c => c.Id == id);
					 id += classesInWeek)
				{
					result.Add(new GroupClass
					{
						Group = item.Group,
						Class = classesList.First(c => c.Id == id)
					});
				}
			}

			return result;
		}
	}
}
