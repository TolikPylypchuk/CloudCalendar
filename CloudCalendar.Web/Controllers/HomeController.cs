using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace CloudCalendar.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return this.View();
		}

		public IActionResult Error()
		{
			this.ViewData["RequestId"] =
				Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
			return this.View();
		}
	}
}
