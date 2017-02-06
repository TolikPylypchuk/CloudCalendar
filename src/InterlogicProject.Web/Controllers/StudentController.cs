using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
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
		private Student currentStudent;

		public StudentController(
			IRepository<Student> students,
			IHttpContextAccessor accessor)
		{
			string userId = accessor.HttpContext.User.FindFirst(
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
	}
}
