using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.LecturersClasses))]
	public class LecturerClass : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть викладача")]
		public int LecturerId { get; set; }

		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[ForeignKey(nameof(LecturerId))]
		public Lecturer Lecturer { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		public override string ToString()
			=> $"{this.Lecturer}, {this.Class}";
	}
}
