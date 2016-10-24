using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Areas.Student.Controllers
{
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
		public IActionResult Index() => this.View();
	}
}
