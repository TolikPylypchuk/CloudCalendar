using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for notifications.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class NotificationsController : Controller
	{
		private IRepository<Notification> notifications;
		private IRepository<UserNotification> userNotifications;
		private IRepository<GroupClass> groupClasses;
		private IRepository<LecturerClass> lecturerClasses;

		/// <summary>
		/// Initializes a new instance of the NotificationsController class.
		/// </summary>
		/// <param name="notifications">
		/// The repository of notifications that this instance will use.
		/// </param>
		/// <param name="userNotifications">
		/// The repository of user-notification relations
		/// that this instance will use.
		/// </param>
		/// <param name="groupClasses">
		/// The repository of group-class relations
		/// that this instance will use.
		/// </param>
		/// <param name="lecturerClasses">
		/// The repository of lecturer-class relations
		/// that this instance will use.
		/// </param>
		public NotificationsController(
			IRepository<Notification> notifications,
			IRepository<UserNotification> userNotifications,
			IRepository<GroupClass> groupClasses,
			IRepository<LecturerClass> lecturerClasses)
		{
			this.notifications = notifications;
			this.userNotifications = userNotifications;
			this.groupClasses = groupClasses;
			this.lecturerClasses = lecturerClasses;
		}

		/// <summary>
		/// Gets all notifications from the database.
		/// </summary>
		/// <returns>All notifications from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public IEnumerable<NotificationDto> GetAll()
			=> this.userNotifications.GetAll()
									?.OrderBy(n => n.Notification.DateTime)
									 .ProjectTo<NotificationDto>();

		/// <summary>
		/// Gets a notification with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the notification to get.</param>
		/// <returns>A notification with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(NotificationDto))]
		public NotificationDto GetById(int id)
			=> Mapper.Map<NotificationDto>(this.userNotifications.GetById(id));

		/// <summary>
		/// Gets all notifications for the specified user.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>All notifications for the specified user.</returns>
		[HttpGet("userId/{userId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public IEnumerable<NotificationDto> GetForUser(
			[FromRoute] string userId)
			=> this.userNotifications.GetAll()
								?.Where(n => n.UserId == userId)
								 .OrderBy(n => n.Notification.DateTime)
								 .ProjectTo<NotificationDto>();
		
		/// <summary>
		/// Gets all notifications between the specified dates.
		/// </summary>
		/// <param name="start">The start of the date range.</param>
		/// <param name="end">The end of the date range.</param>
		/// <returns>All notifications between the specified date.</returns>
		[HttpGet("range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public IEnumerable<NotificationDto> GetWithRange(
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.userNotifications.GetAll()
								?.Where(n =>
									n.Notification.DateTime >= start.Date &&
									n.Notification.DateTime <= end.Date)
								 .ProjectTo<NotificationDto>();

		/// <summary>
		/// Gets all notifications for the specified user
		/// between the specified dates.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <param name="start">The start of the date range.</param>
		/// <param name="end">The end of the date range.</param>
		/// <returns>All notifications between the specified date.</returns>
		[HttpGet("userId/{userId}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public IEnumerable<NotificationDto> GetForUserWithRange(
			[FromRoute] string userId,
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.userNotifications.GetAll()
								?.Where(n => n.UserId == userId)
								 .Where(n =>
									n.Notification.DateTime >= start.Date &&
									n.Notification.DateTime <= end.Date)
								 .ProjectTo<NotificationDto>();

		/// <summary>
		/// Adds a new notification to the database.
		/// </summary>
		/// <param name="notificationDto"></param>
		[HttpPost]
		public IActionResult Post([FromBody] NotificationDto notificationDto)
		{
			if (notificationDto?.Text == null ||
				String.IsNullOrEmpty(notificationDto.UserId) ||
				notificationDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var notification =
				this.notifications.GetAll().FirstOrDefault(
					n => n.Text == notificationDto.Text &&
						 n.DateTime == notificationDto.DateTime)
				?? new Notification
				{
					DateTime = notificationDto.DateTime,
					Text = notificationDto.Text
				};
			
			var notificationToAdd = new UserNotification
			{
				Notification = notification,
				UserId = notificationDto.UserId,
				IsSeen = false
			};

			this.userNotifications.Add(notificationToAdd);

			notificationDto.Id = notificationToAdd.Id;

			return this.CreatedAtRoute(
				"GetNotificationById",
				new { id = notificationDto.Id },
				notificationDto);
		}

		/// <summary>
		/// Adds a new notification for every student
		/// of every group of the specified class.
		/// </summary>
		/// <param name="classId">The ID of the specified class.</param>
		/// <param name="notificationDto">The notification to add.</param>
		/// <returns></returns>
		[HttpPost("groups/classId/{classId}")]
		public IActionResult PostForGroupsInClass(
			[FromRoute] int classId,
			[FromBody] NotificationDto notificationDto)
		{
			if (notificationDto?.Text == null ||
				notificationDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var notification =
				this.notifications.GetAll().FirstOrDefault(
					n => n.Text == notificationDto.Text &&
						 n.DateTime == notificationDto.DateTime)
				?? new Notification
				{
					DateTime = notificationDto.DateTime,
					Text = notificationDto.Text
				};

			var notificationsToAdd =
				this.groupClasses.GetAll()
								 .Where(gc => gc.ClassId == classId)
								 .Select(gc => gc.Group)
								 .SelectMany(g => g.Students)
								 .Select(s => new UserNotification
								 {
									 Notification = notification,
									 UserId = s.UserId,
									 IsSeen = false
								 });

			if (!notificationsToAdd.Any())
			{
				return this.BadRequest();
			}
			
			this.userNotifications.AddRange(notificationsToAdd);

			notificationDto.Id = notificationsToAdd.FirstOrDefault().Id;

			return this.CreatedAtAction(
				nameof(this.GetById),
				new { id = notificationDto.Id },
				notificationDto);
		}

		/// <summary>
		/// Adds a new notification for every lecturer of the specified class.
		/// </summary>
		/// <param name="classId">The ID of the specified class.</param>
		/// <param name="notificationDto">The notification to add.</param>
		/// <returns></returns>
		[HttpPost("lecturers/classId/{classId}")]
		public IActionResult PostForLecturersInClass(
			[FromRoute] int classId,
			[FromBody] NotificationDto notificationDto)
		{
			if (notificationDto?.Text == null ||
				notificationDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var notification =
				this.notifications.GetAll().FirstOrDefault(
					n => n.Text == notificationDto.Text &&
						 n.DateTime == notificationDto.DateTime)
				?? new Notification
				{
					DateTime = notificationDto.DateTime,
					Text = notificationDto.Text
				};

			var notificationsToAdd =
				this.lecturerClasses.GetAll()
									.Where(lc => lc.ClassId == classId)
									.Select(lc => lc.Lecturer)
									.Select(l => new UserNotification
									{
										Notification = notification,
										UserId = l.UserId,
										IsSeen = false
									});

			if (!notificationsToAdd.Any())
			{
				return this.BadRequest();
			}

			this.userNotifications.AddRange(notificationsToAdd);

			notificationDto.Id = notificationsToAdd.FirstOrDefault().Id;

			return this.CreatedAtAction(
				nameof(this.GetById),
				new { id = notificationDto.Id },
				notificationDto);
		}
		
		/// <summary>
		/// Deletes a notification from the database.
		/// </summary>
		/// <param name="id">The ID of the norification to delete.</param>
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			var notificationToDelete = this.userNotifications.GetById(id);

			if (notificationToDelete == null)
			{
				return this.NotFound();
			}

			this.userNotifications.Delete(notificationToDelete);

			if (!this.userNotifications.GetAll().Any(n =>
					n.NotificationId == notificationToDelete.NotificationId))
			{
				this.notifications.Delete(notificationToDelete.NotificationId);
			}

			return this.NoContent();
		}
	}
}
