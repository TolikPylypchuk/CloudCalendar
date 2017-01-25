using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.ClassPlaces))]
	public class ClassPlace : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть пару")]
		public int ClassId { get; set; }

		[Required(ErrorMessage = "Вкажіть аудиторію")]
		public int ClassroomId { get; set; }
		
		[ForeignKey(nameof(ClassroomId))]
		public Classroom Classroom { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		public override string ToString()
			=> $"{this.Class}, {this.Classroom}";
	}
}
