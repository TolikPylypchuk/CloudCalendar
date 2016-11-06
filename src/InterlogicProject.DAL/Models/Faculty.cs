﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Faculties))]
	public class Faculty : EntityBase
	{
		[Required(ErrorMessage = "Please enter the faculty name")]
		[StringLength(50)]
		public string Name { get; set; }
		
		public virtual ICollection<Department> Departments { get; set; } =
			new HashSet<Department>();
	}
}
