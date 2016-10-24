using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Areas.Lecturer.Controllers
{
	/// <summary>
	/// The controller for lecturer-related actions.
	/// </summary>
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
		/// <summary>
		/// Returns the default view.
		/// </summary>
		/// <returns>The default view.</returns>
		public IActionResult Index() => this.View();
	}
}
