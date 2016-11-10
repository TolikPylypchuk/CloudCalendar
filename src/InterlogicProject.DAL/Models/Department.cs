using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Departments))]
	public class Department : EntityBase
	{
		[Required(ErrorMessage = "Please enter the department name")]
		[StringLength(50)]
		public string Name { get; set; }

		[JsonIgnore]
		[Required(ErrorMessage = "Please specify the faculty")]
		public int FacultyId { get; set; }
		
		[ForeignKey(nameof(FacultyId))]
		public Faculty Faculty { get; set; }
		
		public virtual ICollection<Lecturer> Lecturers { get; set; } =
			new HashSet<Lecturer>();

		public override string ToString() => this.Name;
	}
}
