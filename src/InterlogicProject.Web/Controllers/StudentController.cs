using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	public class StudentController : Controller
	{
		public IActionResult Calendar() => this.PartialView();
		public IActionResult CalendarModalContent() => this.PartialView();
		public IActionResult CalendarModalComments() => this.PartialView();
		public IActionResult CalendarModalHomework() => this.PartialView();
		public IActionResult CalendarModalMaterials() => this.PartialView();
	}
}
