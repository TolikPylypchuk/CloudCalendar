using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

using Microsoft.AspNetCore.Identity;

namespace InterlogicProject.Controllers
{
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
		private IRepository<Student> repo;

		public StudentController(
			IRepository<Student> repo,
			UserManager<User> manager)
		{
			this.repo = repo;
		}

		public IActionResult Index() => this.View();

		public IActionResult Group()
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			return this.View(
				this.repo.GetAll().First(s => s.UserId == userId).Group);
		}
	}
}
