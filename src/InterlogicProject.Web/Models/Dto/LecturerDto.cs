namespace InterlogicProject.Models.Dto
{
	public class LecturerDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		
		public string UserFirstName { get; set; }
		public string UserMiddleName { get; set; }
		public string UserLastName { get; set; }

		public string DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		public bool IsDean { get; set; }
		public bool IsHead { get; set; }
		public bool IsAdmin { get; set; }
	}
}
