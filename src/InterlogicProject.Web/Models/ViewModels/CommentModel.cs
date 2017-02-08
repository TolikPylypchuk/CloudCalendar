using System;

namespace InterlogicProject.Web.Models.ViewModels
{
	public class CommentModel
	{
		public string UserId { get; set; }
		public string ClassId { get; set; }
		public string Text { get; set; }
		public DateTime DateTime { get; set; }
	}
}
