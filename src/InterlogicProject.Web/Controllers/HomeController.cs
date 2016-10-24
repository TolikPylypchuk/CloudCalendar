using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Controllers
{
	/// <summary>
	/// The default controller.
	/// </summary>
	public class HomeController : Controller
	{
		private UserManager<User> userManager;

		/// <summary>
		/// Initializes a new instance of the HomeController class.
		/// </summary>
		/// <param name="manager">
		/// The user manager that this instance will use.
		/// </param>
		public HomeController(UserManager<User> manager)
		{
			this.userManager = manager;
		}

		/// <summary>
		/// Redirects to the login action,
		/// or to the default action for a specific role.
		/// </summary>
		/// <returns>A RedirectToAction result.</returns>
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
