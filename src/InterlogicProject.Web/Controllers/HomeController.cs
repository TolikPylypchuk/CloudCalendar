using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	public class HomeController : Controller
	{ 
		public IActionResult Index() => this.View();
	}
}
