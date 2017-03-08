using Microsoft.AspNetCore.Http;

namespace InterlogicProject.Web.Models.ViewModels
{
	public class MaterialViewModel
	{
		public IFormFile File { get; set; }
		public int ClassId { get; set; }
	}
}
