using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Students))]
	public class Student : EntityBase
	{
		[Required(ErrorMessage = "Please specify the user info")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "Please specify the group")]
		public int GroupId { get; set; }
		
		[Required(ErrorMessage = "Please specify the transcript number")]
		public string TranscriptNumber { get; set; }

		[Required(ErrorMessage =
			"Please specify whether this student " +
			"is a leader of a group")]
		public bool IsGroupLeader { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }

		public override string ToString() => this.User.FullName;
	}
}
