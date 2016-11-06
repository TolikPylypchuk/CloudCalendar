using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Controllers
{
	public class HomeController : Controller
	{
		private UserManager<User> userManager;

		public HomeController(UserManager<User> manager)
		{
			this.userManager = manager;
		}

		[Authorize]
		public async Task<IActionResult> Index()
		{
			var user = await this.userManager
				.GetUserAsync(this.HttpContext.User);

			return this.RedirectToAction(
				"Index",
				$"{(await this.userManager.GetRolesAsync(user))[0]}");
		}
	}
}
