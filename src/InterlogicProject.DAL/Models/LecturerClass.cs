using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.LecturersClasses))]
	public class LecturerClass : EntityBase
	{
		public int LecturerId { get; set; }
		public int ClassId { get; set; }

		[ForeignKey(nameof(LecturerId))]
		public Lecturer Lecturer { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }
	}
}
