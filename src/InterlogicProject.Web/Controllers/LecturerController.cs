using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterlogicProject.Web.Controllers
{
	[Authorize(Roles = "Lecturer")]
	public class LecturerController : Controller
	{
	}
}
