namespace CloudCalendar.Schedule.Models
{
	public class Lecturer : EntityBase
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Position { get; set; }
		public Faculty Faculty { get; set; }
	}
}
