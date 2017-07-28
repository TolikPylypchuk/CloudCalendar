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
		
		[Required(ErrorMessage = "Вкажіть дату і час пари")]
		public DateTime DateTime { get; set; }
		
		[Required(ErrorMessage = "Вкажіть тип пари")]
		[StringLength(15)]
		public string Type { get; set; }

		[Required(ErrorMessage =
			"Вкажіть, чи дозволено завантажувати домашнє завдання")]
		public bool HomeworkEnabled { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public Subject Subject { get; set; }

		public virtual ICollection<GroupClass> Groups { get; set; } =
			new HashSet<GroupClass>();

		public virtual ICollection<ClassPlace> Places { get; set; } =
			new HashSet<ClassPlace>();

		public virtual ICollection<LecturerClass> Lecturers { get; set; } =
			new HashSet<LecturerClass>();

		public virtual ICollection<Comment> Comments { get; set; } =
			new HashSet<Comment>();
		
		public override string ToString() => this.Subject.Name;
	}
}
