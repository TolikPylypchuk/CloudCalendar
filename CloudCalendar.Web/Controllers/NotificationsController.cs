using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Web.Models.Dto;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for notifications.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class NotificationsController : Controller
	{
		private UserManager<User> manager;
		private IHttpContextAccessor accessor;

		private IRepository<Notification> notifications;
		private IRepository<UserNotification> userNotifications;
		private IRepository<GroupClass> groupClasses;
		private IRepository<LecturerClass> lecturerClasses;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="NotificationsController"/> class.
		/// </summary>
		/// <param name="manager">
		/// The user manager that this instance will use.
		/// </param>
		/// <param name="accessor">
		/// The HTTP context accessor that this instance will use.
		/// </param>
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
			UserManager<User> manager,
			IHttpContextAccessor accessor,
			IRepository<Notification> notifications,
			IRepository<UserNotification> userNotifications,
			IRepository<GroupClass> groupClasses,
			IRepository<LecturerClass> lecturerClasses)
		{
			this.manager = manager;
			this.accessor = accessor;

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
		/// Gets all notifications for the current user.
		/// </summary>
		/// <returns>All notifications for the current user.</returns>
		[HttpGet("user/current")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public async Task<IEnumerable<NotificationDto>> GetForCurrentUser()
		{
			string name = this.accessor.HttpContext.User.FindFirst(
				ClaimTypes.NameIdentifier)?.Value;

			var user = await this.manager.FindByNameAsync(name);

			return this.userNotifications.GetAll()
										?.Where(n => n.UserId == user.Id)
										 .OrderBy(n => n.Notification.DateTime)
										 .ProjectTo<NotificationDto>();
		}
		
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
		/// <param name="notificationDto">The notification to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
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
					Text = notificationDto.Text,
					ClassId = notificationDto.ClassId
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
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost("groups/classId/{classId}")]
		[SwaggerResponse(201)]
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
					Text = notificationDto.Text,
					ClassId = notificationDto.ClassId
				};

			var notificationsToAdd =
				from gc in this.groupClasses.GetAll()
				where gc.ClassId == classId
				select gc.Group into g
				from s in g.Students
				where String.IsNullOrEmpty(notificationDto.UserId) ||
				      s.UserId != notificationDto.UserId
				select new UserNotification
				{
					Notification = notification,
					UserId = s.UserId,
					IsSeen = false
				};
			
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
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost("lecturers/classId/{classId}")]
		[SwaggerResponse(201)]
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
					Text = notificationDto.Text,
					ClassId = notificationDto.ClassId
				};

			var notificationsToAdd =
				from lc in this.lecturerClasses.GetAll()
				where lc.ClassId == classId
				select lc.Lecturer into l
				where String.IsNullOrEmpty(notificationDto.UserId) ||
				      l.UserId != notificationDto.UserId
				select new UserNotification
				{
					Notification = notification,
					UserId = l.UserId,
					IsSeen = false
				};

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
		/// Marks the specified notification as seen.
		/// </summary>
		/// <param name="id">The ID of the notification to mark.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}/mark/seen")]
		[SwaggerResponse(204)]
		public IActionResult MarkAsSeen(int id)
		{
			var notification = this.userNotifications.GetById(id);
			notification.IsSeen = true;
			this.userNotifications.Update(notification);

			return this.NoContent();
		}

		/// <summary>
		/// Marks the specified notification as not seen.
		/// </summary>
		/// <param name="id">The ID of the notification to mark.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}/mark/notSeen")]
		[SwaggerResponse(204)]
		public IActionResult MarkAsNotSeen(int id)
		{
			var notification = this.userNotifications.GetById(id);
			notification.IsSeen = false;
			this.userNotifications.Update(notification);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a notification from the database.
		/// </summary>
		/// <param name="id">The ID of the norification to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
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
