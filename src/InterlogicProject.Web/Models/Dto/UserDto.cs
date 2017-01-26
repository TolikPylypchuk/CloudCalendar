﻿namespace InterlogicProject.Web.Models.Dto
{
	public class UserDto
	{
		public string Id { get; set; }
		public string Email { get; set; }

		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }

		public override string ToString() => this.FullName;
	}
}
