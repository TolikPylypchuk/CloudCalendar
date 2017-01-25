using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Faculties))]
	public class Faculty : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву факультету")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage =
			"Вкажіть корпус, в якому знаходиться цей факультет")]
		public int BuildingId { get; set; }

		[ForeignKey(nameof(BuildingId))]
		public Building Building { get; set; }
		
		public virtual ICollection<Department> Departments { get; set; } =
			new HashSet<Department>();

		public override string ToString() => this.Name;
	}
}
