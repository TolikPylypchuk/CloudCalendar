using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Route("templates/[controller]/[action]")]
	public class ViewController : Controller
	{
		public IActionResult Groups() => this.PartialView();
		public IActionResult Group() => this.PartialView();
		public IActionResult Lecturers() => this.PartialView();
		public IActionResult Lecturer() => this.PartialView();
		public IActionResult Notifications() => this.PartialView();
	}
}
