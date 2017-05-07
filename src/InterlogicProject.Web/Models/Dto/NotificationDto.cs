using System;

namespace InterlogicProject.Web.Models.Dto
{
	public class NotificationDto
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public bool? IsSeen { get; set; }
		public DateTime DateTime { get; set; }

		public string UserId { get; set; }
		public string UserFirstName { get; set; }
		public string UserMiddleName { get; set; }
		public string UserLastName { get; set; }

		public override string ToString() => this.Text;
	}
}
