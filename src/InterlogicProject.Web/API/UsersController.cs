using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for users.
	/// </summary>
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private UserManager<User> manager;
		private AppDbContext context;
		private IHttpContextAccessor accessor;

		/// <summary>
		/// Initializes a new instance of the UsersController class.
		/// </summary>
		/// <param name="manager">
		/// The manager that this instance will use.
		/// </param>
		/// <param name="context">
		/// The DB context that this instance will use.
		/// </param>
		/// <param name="accessor">
		/// The HTTP context accessor that this instance will use.
		/// </param>
		public UsersController(
			UserManager<User> manager,
			AppDbContext context,
			IHttpContextAccessor accessor)
		{
			this.manager = manager;
			this.context = context;
			this.accessor = accessor;
		}

		/// <summary>
		/// Gets all users from the database.
		/// </summary>
		/// <returns>All users from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<UserDto>))]
		public IEnumerable<UserDto> Get()
			=> this.context.Users?.ProjectTo<UserDto>();

		/// <summary>
		/// Gets a user with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the user to get.</param>
		/// <returns>A user with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
		public async Task<UserDto> Get(string id)
			=> Mapper.Map<UserDto>(await this.manager.FindByIdAsync(id));

		/// <summary>
		/// Gets the currently logged in user.
		/// </summary>
		/// <returns>The currently logged in user.</returns>
		[HttpGet("current")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
		public async Task<UserDto> GetCurrent()
		{
			var id = accessor.HttpContext.User.FindFirst(
				ClaimTypes.NameIdentifier)?.Value;
			return Mapper.Map<UserDto>(await this.manager.FindByIdAsync(id));
		}
	}
}
