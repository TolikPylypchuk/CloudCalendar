using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.Notifications))]
	public class Notification : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть текст повідомлення")]
		public int TextId { get; set; }

		[Required(ErrorMessage = "Вкажіть користувача")]
		public string UserId { get; set; }

		public bool IsSeen { get; set; }

		[ForeignKey(nameof(TextId))]
		public NotificationText Text { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
