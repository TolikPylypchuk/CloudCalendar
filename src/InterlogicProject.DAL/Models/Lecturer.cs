using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Lecturers))]
	public class Lecturer : EntityBase
	{
		[Required(ErrorMessage = "Please specify the user info")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "Please specify the department")]
		public int DepartmentId { get; set; }

		[Required(ErrorMessage =
			"Please specify whether this lecturer " +
			"is a head of a department")]
		public bool IsHead { get; set; }

		[Required(ErrorMessage =
			"Please specify whether this lecturer " +
			"is a dean of a faculty")]
		public bool IsDean { get; set; }

		[Required(ErrorMessage =
			"Please specify whether this lecturer " +
			"has administrator privileges")]
		public bool IsAdmin { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public Department Department { get; set; }

		public override string ToString() => this.User.FullName;
	}
}
