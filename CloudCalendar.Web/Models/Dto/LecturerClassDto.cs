namespace CloudCalendar.Web.Models.Dto
{
	public class LecturerClassDto
	{
		public int Id { get; set; }
		public int LecturerId { get; set; }
		public int ClassId { get; set; }

		public override string ToString()
			=> $"{this.LecturerId}, {this.ClassId}";
	}
}
