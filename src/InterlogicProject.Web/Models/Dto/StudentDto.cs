namespace InterlogicProject.Models.Dto
{
	public class StudentDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		
		public string UserFirstName { get; set; }
		public string UserMiddleName { get; set; }
		public string UserLastName { get; set; }

		public int GroupId { get; set; }
		public string GroupName { get; set; }
		public int NumberInGroup { get; set; }
		public bool IsGroupLeader { get; set; }

		public string StudentNumber { get; set; }
	}
}
