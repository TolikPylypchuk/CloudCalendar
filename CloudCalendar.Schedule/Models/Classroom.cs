namespace CloudCalendar.Schedule.Models
{
	public class Classroom : EntityBase
	{
		public string Number { get; set; }
		public int Capacity { get; set; }
		public ClassroomType Type { get; set; }
		public Building Building { get; set; }
	}
}
