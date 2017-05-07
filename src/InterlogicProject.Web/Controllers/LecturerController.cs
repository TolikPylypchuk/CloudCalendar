using System.Linq;
using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;
using InterlogicProject.Web.Models.ViewModels;

namespace InterlogicProject.Web.Controllers
{
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
		private IRepository<Faculty> faculties;

		private Lecturer currentLecturer;

		public LecturerController(
			IRepository<Faculty> faculties,
			IRepository<Lecturer> lecturers,
			IHttpContextAccessor accessor)
		{
			this.faculties = faculties;

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
	}
}
