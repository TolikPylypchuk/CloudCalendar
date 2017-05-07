using System.Collections.Generic;
using System.Linq;

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
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class NotificationsController : Controller
	{
		private IRepository<Notification> notifications;

		/// <summary>
		/// Initializes a new instance of the NotificationsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public NotificationsController(
			IRepository<Notification> repo)
		{
			this.notifications = repo;
		}

		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<NotificationDto>))]
		public IEnumerable<NotificationDto> Get()
			=> this.notifications.GetAll().ProjectTo<NotificationDto>();

		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(NotificationDto))]
		public NotificationDto Get(int id)
			=> Mapper.Map<NotificationDto>(this.notifications.GetById(id));

		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
