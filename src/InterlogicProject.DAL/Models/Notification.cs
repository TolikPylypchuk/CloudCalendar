using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Notifications))]
	public class Notification : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть текст сповіщення")]
		[StringLength(300)]
		public string Text { get; set; }

		[Required(ErrorMessage = "Вкажіть дату і час сповіщення")]
		public DateTime DateTime { get; set; }
	}
}
