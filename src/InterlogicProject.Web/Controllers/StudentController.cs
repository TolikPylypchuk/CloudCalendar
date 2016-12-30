using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.ViewModels;

using Microsoft.AspNetCore.Http;

namespace InterlogicProject.Web.Controllers
{
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
		private IRepository<Student> students;
		private IRepository<Group> groups;
		private UserManager<User> manager;

		private Student currentStudent;

		public StudentController(
			IRepository<Student> students,
			IRepository<Group> groups,
			UserManager<User> manager,
			IHttpContextAccessor accessor)
		{
			this.students = students;
			this.groups = groups;
			this.manager = manager;

			var userId = accessor.HttpContext.User.FindFirst(
				ClaimTypes.NameIdentifier).Value;

			this.currentStudent = students.GetAll()
				.FirstOrDefault(l => l.UserId == userId);
		}

		public IActionResult Index()
			=> this.View(new StudentModel
			{
				Student = this.currentStudent
			});

		public IActionResult Group()
			=> this.View(new StudentModel
			{
				Student = this.currentStudent
			});

		public IActionResult Create()
			=> this.RedirectToAction(nameof(Edit));

		public IActionResult Edit()
			=> this.View(new EditStudentModel
			{
				CurrentStudent = this.currentStudent,
				IsEditing = false
			});

		public IActionResult Edit(int id)
		{
			var student = this.students.GetById(id);

			if (student == null)
			{
				return this.View(new EditStudentModel
				{
					CurrentStudent = this.currentStudent,
					IsEditing = false
				});
			}

			return this.View(new EditStudentModel
			{
				CurrentStudent = this.currentStudent,
				IsEditing = true,
				Id = id,
				FirstName = student.User.FirstName,
				MiddleName = student.User.MiddleName,
				LastName = student.User.LastName,
				Email = student.User.Email,
				GroupName = student.Group.Name,
				TranscriptNumber = student.TranscriptNumber,
				IsGroupLeader = student.IsGroupLeader
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditStudentModel model)
		{
			model.CurrentStudent = this.currentStudent;
			
			if (!model.Email?.EndsWith($"@{Program.EmailDomain}") ?? false)
			{
				this.ModelState.AddModelError(
					"email", $"У email має бути домен {Program.EmailDomain}");
			}

			var group = this.groups.GetAll()
					.FirstOrDefault(g => g.Name == model.GroupName);

			if (model.GroupName != null && group == null)
			{
				this.ModelState.AddModelError(
					"group", "Ви вказали неіснуючу групу");
			}

			var leader = group?.Students.FirstOrDefault(s => s.IsGroupLeader);

			if (leader != null && model.IsGroupLeader)
			{
				this.ModelState.AddModelError(
					"leader", "В цій групі вже є староста");
			}

			var transcriptStudent = this.students.GetAll().FirstOrDefault(
				s => s.TranscriptNumber == model.TranscriptNumber);

			if (transcriptStudent != null)
			{
				this.ModelState.AddModelError(
					"transcript",
					"Студент із таким номером заліковки вже існує");
			}

			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			if (model.IsEditing)
			{
				var student = this.students.GetById(model.Id);

				student.User.FirstName = model.FirstName;
				student.User.MiddleName = model.MiddleName;
				student.User.LastName = model.LastName;
				student.User.Email = model.Email;
				student.User.NormalizedEmail = model.Email?.ToUpper();
				student.Group = group;
				student.TranscriptNumber = model.TranscriptNumber;
				student.IsGroupLeader = model.IsGroupLeader;

				this.students.Update(student);
			} else
			{
				var student = new Student
				{
					User = new User
					{
						FirstName = model.FirstName,
						MiddleName = model.MiddleName,
						LastName = model.LastName,
						Email = model.Email,
						NormalizedEmail = model.Email?.ToUpper()
					},
					Group = group,
					TranscriptNumber = model.TranscriptNumber,
					IsGroupLeader = model.IsGroupLeader
				};

				await this.manager.CreateAsync(student.User, "secret");

				this.students.Add(student);
			}

			return this.RedirectToAction(nameof(Group));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var student = this.students.GetById(id);
			this.students.Delete(student);
			await this.manager.DeleteAsync(student.User);
			return this.RedirectToAction(nameof(Group));
		}
	}
}
