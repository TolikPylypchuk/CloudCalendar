using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Classes))]
	public class Class : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть дані про предмет")]
		public int GroupSubjectId { get; set; }

		[Required(ErrorMessage = "Вкажіть дату і час пари")]
		public DateTime DateTime { get; set; }

		[Required(ErrorMessage = "Вкажіть корпус")]
		public string Building { get; set; }

		[Required(ErrorMessage = "Вкажіть кабінет")]
		public string Classroom { get; set; }

		[Required(ErrorMessage = "Вкажіть тип пари")]
		public string Type { get; set; }

		[ForeignKey(nameof(GroupSubjectId))]
		public GroupSubject GroupSubject { get; set; }

		public override string ToString() => this.GroupSubject.Subject.Name;
	}
}
