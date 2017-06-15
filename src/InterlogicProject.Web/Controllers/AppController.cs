using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Route("templates/[action]")]
	public class AppController : Controller
	{
		public IActionResult App() => this.PartialView();
		public IActionResult Navigation() => this.PartialView();
	}
}
