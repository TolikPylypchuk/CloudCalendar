namespace InterlogicProject.Web.Models.Dto
{
	public class GroupClassDto
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public int ClassId { get; set; }

		public override string ToString()
			=> $"{this.GroupId}, {this.ClassId}";
	}
}
