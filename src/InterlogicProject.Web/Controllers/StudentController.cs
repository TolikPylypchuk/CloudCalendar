using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Areas.Student.Controllers
{
	/// <summary>
	/// The controller for student-related actions.
	/// </summary>
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
	{
		/// <summary>
		/// Returns the default view.
		/// </summary>
		/// <returns>The default view.</returns>
		public IActionResult Index() => this.View();
	}
}
