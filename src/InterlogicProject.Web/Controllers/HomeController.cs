using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return this.View();
		}
	}
}
