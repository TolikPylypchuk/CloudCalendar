using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Route("templates/[controller]/[action]")]
	public class ViewController : Controller
	{
		public IActionResult Notifications() => this.PartialView();
	}
}
