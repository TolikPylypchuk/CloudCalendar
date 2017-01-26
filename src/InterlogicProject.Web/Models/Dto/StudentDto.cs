namespace InterlogicProject.Web.Models.Dto
{
	public class StudentDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		
		public string UserFirstName { get; set; }
		public string UserMiddleName { get; set; }
		public string UserLastName { get; set; }
		public string UserFullName { get; set; }

		public int GroupId { get; set; }
		public string GroupName { get; set; }
		public bool IsGroupLeader { get; set; }

		public string TranscriptNumber { get; set; }

		public override string ToString() => this.UserFullName;
	}
}
