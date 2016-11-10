using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Groups))]
	public class Group : EntityBase
	{
		[Required(ErrorMessage = "Please enter the group name")]
		public string Name { get; set; }
		
		[Range(1, 6, ErrorMessage = "The year must be in range 1 to 6")]
		[Required(ErrorMessage = "Please enter the year")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Please specify the curator")]
		public int CuratorId { get; set; }

		[Required(ErrorMessage = "Please specify the department")]
		public int DepartmentId { get; set; }
		
		[ForeignKey(nameof(CuratorId))]
		public Lecturer Curator { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public Department Department { get; set; }

		public virtual ICollection<Student> Students { get; set; } =
			new HashSet<Student>();

		public override string ToString() => this.Name;
	}
}
