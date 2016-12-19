using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Classes))]
	public class Class : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть назву предмету")]
		public int SubjectId { get; set; }

		[Required(ErrorMessage = "Вкажіть групу")]
		public int GroupId { get; set; }

		[Required(ErrorMessage = "Вкажіть дату і час пари")]
		public DateTime DateTime { get; set; }
		
		[Required(ErrorMessage = "Вкажіть тип пари")]
		public string Type { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public Subject Subject { get; set; }

		[ForeignKey(nameof(GroupId))]
		public Group Group { get; set; }

		public virtual ICollection<ClassPlace> Places { get; set; } =
			new HashSet<ClassPlace>();

		public virtual ICollection<LecturerClass> Lecturers { get; set; } =
			new HashSet<LecturerClass>();

		public override string ToString() => this.Subject.Name;
	}
}
