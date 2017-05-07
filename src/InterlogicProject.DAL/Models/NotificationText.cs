using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.NotificationTexts))]
	public class NotificationText : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть текст сповіщення")]
		[StringLength(300)]
		public string Text { get; set; }
	}
}
