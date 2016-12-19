using System.Linq;
using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Models.Dto;
using InterlogicProject.Models.ViewModels;

namespace InterlogicProject.Controllers
{
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
		private IRepository<Group> groups;

		private Lecturer currentLecturer;

		public LecturerController(
			IRepository<Group> groups,
			IRepository<Lecturer> lecturers,
			IHttpContextAccessor accessor)
		{
			this.groups = groups;

			var userId = accessor.HttpContext.User.FindFirst(
				ClaimTypes.NameIdentifier).Value;

			this.currentLecturer = lecturers.GetAll()
				.First(l => l.UserId == userId);
		}

		public IActionResult Index()
		{
			var model = new LecturerModel
			{
				Lecturer = Mapper.Map<LecturerDto>(this.currentLecturer)
			};

			return this.View(model);
		}

		public IActionResult Department()
			=> this.View(this.currentLecturer.Department);

		public IActionResult Groups()
			=> this.View(this.groups.GetAll()
				.Where(g => g.Curator.Department ==
							this.currentLecturer.Department));
	}
}
