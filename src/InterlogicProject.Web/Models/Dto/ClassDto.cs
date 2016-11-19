using System;

namespace InterlogicProject.Models.Dto
{
	public class ClassDto
	{
		public int Id { get; set; }
		public int GroupSubjectId { get; set; }
		public DateTime DateTime { get; set; }
		public string Building { get; set; }
		public string Classroom { get; set; }
		public string Type { get; set; }
	}
}
