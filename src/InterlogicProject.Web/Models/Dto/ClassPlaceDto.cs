namespace InterlogicProject.Web.Models.Dto
{
	public class ClassPlaceDto
	{
		public int Id { get; set; }
		public int ClassId { get; set; }
		public int ClassroomId { get; set; }

		public override string ToString()
			=> $"{this.ClassId}, {this.ClassroomId}";
	}
}
