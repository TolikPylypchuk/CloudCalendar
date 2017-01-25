using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Comments))]
	public class Comment : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть пару, якій належить цей коментар")]
		public int ClassId { get; set; }

		[Required(ErrorMessage = "Вкажіть користувача, який залишив коментар")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "Коментар порожній")]
		[StringLength(300)]
		public string Text { get; set; }

		[Required(ErrorMessage = "Вкажіть дату і час коментаря")]
		public DateTime DateTime { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
