using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	public class AppController : Controller
	{
		public IActionResult App() => this.PartialView();
		public IActionResult Navigation() => this.PartialView();
	}
}
