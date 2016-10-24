using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.Models.ViewModels;

namespace InterlogicProject.Controllers
{
	/// <summary>
	/// The controller for account-related actions.
	/// </summary>
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<User> userManager;
		private SignInManager<User> signInManager;

		/// <summary>
		/// Initializes a new instance of the AccountController class.
		/// </summary>
		/// <param name="userManager">
		/// The user manager that this instance will use.
		/// </param>
		/// <param name="signInManager">
		/// The sign in manager that this instance will use.
		/// </param>
		public AccountController(
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		/// <summary>
		/// Gets the Login view or redirects
		/// to default action if already logged in.
		/// </summary>
		/// <param name="returnUrl">The URL to return to.</param>
		/// <returns>
		/// The Login view or redirection
		/// to default action if already logged in.</returns>
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

		/// <summary>
		/// Logs in a user.
		/// </summary>
		/// <param name="details">The login details.</param>
		/// <param name="returnUrl">The URL to return to.</param>
		/// <returns>
		/// Redirection to a return URL if login was successful
		/// or a Login view if login failed.
		/// </returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(
			LoginModel details,
			string returnUrl)
		{
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

		/// <summary>
		/// Logs out a user.
		/// </summary>
		/// <returns>Redirection to the default action.</returns>
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();
			return this.RedirectToAction(nameof(HomeController.Index), "Home");
		}

		/// <summary>
		/// Returns a view that tells the user that access was denied.
		/// </summary>
		/// <returns>
		/// A view that tells the user that access was denied.
		/// </returns>
		[AllowAnonymous]
		public IActionResult AccessDenied() => this.View();
	}
}
