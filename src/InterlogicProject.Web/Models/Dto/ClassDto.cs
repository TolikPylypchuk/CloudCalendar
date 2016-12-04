using System;

namespace InterlogicProject.Models.Dto
{
	public class ClassDto
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
		public DateTime DateTime { get; set; }
		public string Type { get; set; }
	}
}
