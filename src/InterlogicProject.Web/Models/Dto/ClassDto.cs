using System;

namespace InterlogicProject.Web.Models.Dto
{
	public class ClassDto
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
		public DateTime DateTime { get; set; }
		public string Type { get; set; }

		public override string ToString() => this.SubjectName;
	}
}
