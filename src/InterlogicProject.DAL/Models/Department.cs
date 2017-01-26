using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Departments))]
	public class Department : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву кафедри")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Вкажіть факультет кафедри")]
		public int FacultyId { get; set; }
		
		[ForeignKey(nameof(FacultyId))]
		public Faculty Faculty { get; set; }
		
		public virtual ICollection<Lecturer> Lecturers { get; set; } =
			new HashSet<Lecturer>();

		public override string ToString() => this.Name;
	}
}
