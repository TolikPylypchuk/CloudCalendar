using System;
using System.Collections.Generic;

namespace InterlogicProject.ScheduleClient.Models
{
	public class Class : EntityBase
	{
		public DayOfWeek DayOfWeek { get; set; }
		public int Number { get; set; }
		public string Frequency { get; set; }
		public string Type { get; set; }
		public int Year { get; set; }
		public int Semester { get; set; }
		public List<Classroom> Classrooms { get; set; }
		public List<Group> Groups { get; set; }
		public List<Lecturer> Lecturers { get; set; }
	}
}
