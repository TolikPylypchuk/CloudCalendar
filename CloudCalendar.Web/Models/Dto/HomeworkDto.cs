using System;

namespace CloudCalendar.Web.Models.Dto
{
	public class HomeworkDto
	{
		public int Id { get; set; }
		public string FileName { get; set; }
		public int StudentId { get; set; }
		public int ClassId { get; set; }
		public DateTime DateTime { get; set; }
		public bool? Accepted { get; set; }
	}
}
