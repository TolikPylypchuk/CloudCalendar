using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Departments))]
	public class Department : EntityBase
	{
		[Required(ErrorMessage = "Please enter the department name")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please specify the manager")]
		public int ManagerId { get; set; }

		[Required(ErrorMessage = "Please specify the faculty")]
		public int FacultyId { get; set; }

		[ForeignKey(nameof(ManagerId))]
		public Lecturer Manager { get; set; }

		[ForeignKey(nameof(FacultyId))]
		public Faculty Faculty { get; set; }
	}
}
