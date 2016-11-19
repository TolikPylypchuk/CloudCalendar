namespace InterlogicProject.Models.Dto
{
	public class GroupSubjectDto
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public int LecturerId { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
	}
}
