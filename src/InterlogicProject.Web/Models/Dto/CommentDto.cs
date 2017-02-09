using System;

namespace InterlogicProject.Web.Models.Dto
{
	public class CommentDto
	{
		public int Id { get; set; }
		public int ClassId { get; set; }
		public string UserId { get; set; }
		public string UserFirstName { get; set; }
		public string UserMiddleName { get; set; }
		public string UserLastName { get; set; }
		public string Text { get; set; }
		public DateTime DateTime { get; set; }

		public override string ToString()
			=> this.Text.Length <= 20
				? $"{this.Text}; {this.UserLastName} {this.UserFirstName}"
				: $"{this.Text.Substring(20)}...; {this.UserLastName} " +
				  $"{this.UserFirstName}";
	}
}
