using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using InterlogicProject.Web.Models.ViewModels;
using InterlogicProject.Web.Services;

namespace InterlogicProject.Web.Controllers
{
	[Route("templates/[controller]/[action]")]
	public class AccountController : Controller
	{
		private string emailDomain;

		public AccountController(IOptionsSnapshot<Settings> settings)
		{
			this.emailDomain = $"@{settings.Value.EmailDomain}";
		}

		public IActionResult Login()
			=> this.PartialView(
				new LoginPageModel { EmailDomain = this.emailDomain });
	}
}
