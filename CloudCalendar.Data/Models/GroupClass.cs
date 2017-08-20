using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.GroupsClasses))]
	public class GroupClass : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть групу")]
		public int GroupId { get; set; }

		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
		
		public override string ToString()
			=> $"{this.Group}, {this.Class}";
	}
}
