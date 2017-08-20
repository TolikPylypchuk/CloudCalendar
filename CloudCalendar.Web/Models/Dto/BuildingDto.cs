namespace CloudCalendar.Web.Models.Dto
{
	public class BuildingDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }

		public override string ToString()
			=> $"{this.Name}, {this.Address}";
	}
}
