using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.ClassPlaces))]
	public class ClassPlace : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[Required(ErrorMessage = "Вкажіть корпус")]
		public string Building { get; set; }

		[Required(ErrorMessage = "Вкажіть кабінет")]
		public string Classroom { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		public override string ToString()
			=> $"{this.Building}, {this.Classroom}";
	}
}
