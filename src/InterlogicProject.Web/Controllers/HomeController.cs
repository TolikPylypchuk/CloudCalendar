using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[ExcludeFromCodeCoverage]
	public class HomeController : Controller
	{
		public IActionResult Index() => this.View();
	}
}
