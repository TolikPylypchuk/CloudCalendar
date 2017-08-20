namespace CloudCalendar.Web.Models.Dto
{
	public class StudentDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }

		public int GroupId { get; set; }
		public string GroupName { get; set; }
		public bool? IsGroupLeader { get; set; }

		public string TranscriptNumber { get; set; }

		public override string ToString() => this.FullName;
	}
}
