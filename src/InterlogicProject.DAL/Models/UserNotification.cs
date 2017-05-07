using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterlogicProject.DAL.Models
{
	[Table(nameof(AppDbContext.UserNotifications))]
	public class UserNotification : EntityBase
	{
		[Required(ErrorMessage = "Вкажіть текст повідомлення")]
		public int NotificationId { get; set; }

		[Required(ErrorMessage = "Вкажіть користувача")]
		public string UserId { get; set; }

		public bool IsSeen { get; set; }

		[ForeignKey(nameof(NotificationId))]
		public Notification Notification { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
