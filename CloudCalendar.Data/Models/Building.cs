﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudCalendar.Data.Models
{
	[Table(nameof(AppDbContext.Buildings))]
	public class Building : EntityBase
	{
		[Required(ErrorMessage = "Введіть назву корпусу")]
		[StringLength(60)]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Введіть адресу корпусу")]
		[StringLength(30)]
		public string Address { get; set; }

		public virtual ICollection<Faculty> Faculties { get; set; } =
			new HashSet<Faculty>();

		public override string ToString()
			=> $"{this.Name}, {this.Address}";
	}
}
