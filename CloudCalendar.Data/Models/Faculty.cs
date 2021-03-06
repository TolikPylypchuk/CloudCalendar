﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.Faculties))]
	public class Faculty : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву факультету")]
		[StringLength(50)]
		public string Name { get; set; }
		
		public virtual ICollection<Department> Departments { get; set; } =
			new HashSet<Department>();

		public override string ToString() => this.Name;
	}
}
