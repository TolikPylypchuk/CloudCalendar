using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.Students))]
	public class Student : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть інформацію про користувача")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "Вкажіть групу")]
		public int GroupId { get; set; }
		
		[Required(ErrorMessage = "Вкажіть номер залікової книжки")]
		[StringLength(10)]
		public string TranscriptNumber { get; set; }

		[Required(ErrorMessage =
			"Вкажіть, чи цей студент є старостою групи")]
		public bool IsGroupLeader { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }

		public override string ToString() => this.User.FullName;
	}
}
