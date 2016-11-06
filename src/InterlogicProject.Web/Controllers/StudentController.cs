using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Controllers
{
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
		public IActionResult Index() => this.View();
	}
}
