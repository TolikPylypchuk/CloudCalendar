using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Login() => this.PartialView();
	}
}
