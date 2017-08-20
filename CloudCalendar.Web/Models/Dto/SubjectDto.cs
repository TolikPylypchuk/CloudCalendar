namespace CloudCalendar.Web.Models.Dto
{
	public class SubjectDto
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString() => this.Name;
	}
}
