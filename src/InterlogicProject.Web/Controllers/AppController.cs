using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	public class AppController : Controller
	{
		public IActionResult Index() => this.PartialView();
		public IActionResult Navigation() => this.PartialView();
	}
}
