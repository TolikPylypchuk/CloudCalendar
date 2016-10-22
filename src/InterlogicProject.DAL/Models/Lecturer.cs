using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Lecturers))]
	public class Lecturer : EntityBase
	{
		[Required(ErrorMessage = "Please specify the account")]
		public int AccountId { get; set; }

		[Required(ErrorMessage = "Please specify the department")]
		public int DepartmentId { get; set; }

		[ForeignKey(nameof(AccountId))]
		public Account Account { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public Department Department { get; set; }
	}
}
