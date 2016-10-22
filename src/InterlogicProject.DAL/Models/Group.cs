using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Groups))]
	public class Group : EntityBase
	{
		[Required(ErrorMessage = "Please enter the name")]
		public string Name { get; set; }
		
		[Range(1, 6, ErrorMessage = "The year must be in range 1 to 6")]
		[Required(ErrorMessage = "Please enter the year")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Please specify the group leader")]
		public int GroupLeaderId { get; set; }
		
		[Required(ErrorMessage = "Please specify the curator")]
		public int CuratorId { get; set; }

		[Required(ErrorMessage = "Please specify the department")]
		public int DepartmentId { get; set; }

		[ForeignKey(nameof(GroupLeaderId))]
		public Student GroupLeader { get; set; }

		[ForeignKey(nameof(CuratorId))]
		public Lecturer Curator { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public Department Department { get; set; }
	}
}
