namespace InterlogicProject.ScheduleClient.Models
{
	public class Group : EntityBase
	{
		public string Name { get; set; }
		public int Year { get; set; }
		public int NumStudents { get; set; }
		public Faculty Faculty { get; set; }
	}
}
