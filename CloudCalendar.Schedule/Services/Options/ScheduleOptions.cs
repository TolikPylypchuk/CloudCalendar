using System.Collections.Generic;

namespace CloudCalendar.Schedule.Services.Options
{
	public class ScheduleOptions
	{
		public string[] ClassStarts { get; set; }
		public string[] ClassEnds { get; set; }
		public string ClassDuration { get; set; }

		public List<Semester> Semesters { get; set; }

		public class Semester
		{
			public string Start { get; set;}
			public string End { get; set; }
		}
	}
}
