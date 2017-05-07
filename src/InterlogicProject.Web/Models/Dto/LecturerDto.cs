namespace InterlogicProject.Web.Models.Dto
{
	public class LecturerDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }

		public int DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		public bool? IsDean { get; set; }
		public bool? IsHead { get; set; }
		public bool? IsAdmin { get; set; }

		public override string ToString() => this.FullName;
	}
}
