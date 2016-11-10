using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.Controllers
{
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
		private IRepository<Group> groupRepo;
		private IRepository<Department> departmentRepo;
		private IRepository<Lecturer> lecturerRepo;

		private Lecturer currentLecturer;

		public LecturerController(
			IRepository<Group> groups,
			IRepository<Department> departments,
			IRepository<Lecturer> lecturers,
			IHttpContextAccessor accessor)
		{
			this.groupRepo = groups;
			this.departmentRepo = departments;
			this.lecturerRepo = lecturers;

			var userId = accessor.HttpContext.User
				.FindFirst(ClaimTypes.NameIdentifier).Value;

			this.currentLecturer = this.lecturerRepo.GetAll()
				.First(l => l.UserId == userId);
		}

		public IActionResult Index() => this.View();

		public IActionResult Department()
			=> this.View(this.currentLecturer.Department);

		public IActionResult Groups()
			=> this.View(this.groupRepo.GetAll()
				.Where(g => g.Department == this.currentLecturer.Department));
	}
}
