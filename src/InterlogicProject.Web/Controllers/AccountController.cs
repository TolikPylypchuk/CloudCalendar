using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.Models.ViewModels;

namespace InterlogicProject.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<User> userManager;
		private SignInManager<User> signInManager;

		public AccountController(
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl)
		{
			if (this.User?.Identity?.IsAuthenticated ?? false)
			{
				return this.RedirectToAction(
					nameof(HomeController.Index),
					"Home");
			}

			this.ViewBag.ReturnUrl = returnUrl;
			return this.View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(
			LoginModel details,
			string returnUrl)
		{
			details.Email += $"@{Program.EmailDomain}";

			if (this.ModelState.IsValid)
			{
				var user = await this.userManager
					.FindByEmailAsync(details.Email);

				if (user != null)
				{
					await this.signInManager.SignOutAsync();
					var result =
						await this.signInManager.PasswordSignInAsync(
							user,
							details.Password,
							false,
							false);

					if (result.Succeeded)
					{
						return this.Redirect(returnUrl ?? "/");
					}
				}
			}

			this.ModelState.AddModelError(
					nameof(LoginModel.Email),
					"Invalid user name or password.");

			return this.View(details);
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();
			return this.RedirectToAction(nameof(HomeController.Index), "Home");
		}

		[AllowAnonymous]
		public IActionResult AccessDenied() => this.View();
	}
}
