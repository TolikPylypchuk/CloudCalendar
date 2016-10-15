using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(ApplicationDbContext.Faculties))]
	public class Faculty : EntityBase
	{
		[Required(ErrorMessage = "Please enter the faculty name")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please specify the dean")]
		public int DeanId { get; set; }

		[ForeignKey(nameof(DeanId))]
		public Lecturer Dean { get; set; }
	}
}
