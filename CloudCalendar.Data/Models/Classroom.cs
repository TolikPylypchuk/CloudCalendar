using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.Classrooms))]
	public class Classroom : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть назву або номер аудиторії")]
		[StringLength(10)]
		public string Name { get; set; }

		[Required(ErrorMessage =
			"Вкажіть корпус, в якому знаходиться ця аудиторія")]
		public int BuildingId { get; set; }

		[ForeignKey(nameof(BuildingId))]
		public Building Building { get; set; }

		public virtual ICollection<ClassPlace> Classes { get; set; } =
			new HashSet<ClassPlace>();

		public override string ToString() => this.Name;
	}
}
