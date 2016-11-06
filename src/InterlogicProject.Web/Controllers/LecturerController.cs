using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Controllers
{
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
		public IActionResult Index() => this.View();
	}
}
