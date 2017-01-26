﻿namespace InterlogicProject.Web.Models.Dto
{
	public class FacultyDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int BuildingId { get; set; }
		public string BuildingName { get; set; }
		public string BuildingAddress { get; set; }

		public override string ToString() => this.Name;
	}
}
