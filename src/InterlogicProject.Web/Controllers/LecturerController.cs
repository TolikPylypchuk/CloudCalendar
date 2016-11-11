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

		private Lecturer currentLecturer;

		public LecturerController(
			IRepository<Group> groups,
			IRepository<Lecturer> lecturers,
			IHttpContextAccessor accessor)
		{
			this.groupRepo = groups;

			var userId = accessor.HttpContext.User
				.FindFirst(ClaimTypes.NameIdentifier).Value;

			this.currentLecturer = lecturers.GetAll()
				.First(l => l.UserId == userId);
		}

		public IActionResult Index() => this.View();

		public IActionResult Department()
			=> this.View(this.currentLecturer.Department);

		public IActionResult Groups()
			=> this.View(this.groupRepo.GetAll()
				.Where(g => g.Curator.Department ==
							this.currentLecturer.Department));
	}
}
