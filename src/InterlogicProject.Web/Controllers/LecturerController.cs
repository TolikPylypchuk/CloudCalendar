using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Route("templates/[controller]")]
	public class LecturerController : Controller
	{
		[Route("[action]")]
		public IActionResult Calendar() => this.PartialView();

		[Route("calendar/modal-content")]
		public IActionResult CalendarModalContent() => this.PartialView();

		[Route("calendar/modal-comments")]
		public IActionResult CalendarModalComments() => this.PartialView();

		[Route("calendar/modal-homework")]
		public IActionResult CalendarModalHomework() => this.PartialView();

		[Route("calendar/modal-materials")]
		public IActionResult CalendarModalMaterials() => this.PartialView();
	}
}
