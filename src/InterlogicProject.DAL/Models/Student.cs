using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Students))]
	public class Student : EntityBase
	{
		[Required(ErrorMessage = "Please specify the account")]
		public int AccountId { get; set; }

		[Required(ErrorMessage = "Please specify the group")]
		public int GroupId { get; set; }

		[Required(ErrorMessage = "Please specify the number in group")]
		public int NumberInGroup { get; set; }

		[Required(ErrorMessage = "Please specify the student number")]
		public string StudentNumber { get; set; }

		[ForeignKey(nameof(AccountId))]
		public Account Account { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }
	}
}
