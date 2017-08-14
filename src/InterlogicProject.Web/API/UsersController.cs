using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.Web.Models.Dto;
using InterlogicProject.Web.Services;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for users.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UsersController : Controller
	{
		private UserManager<User> manager;
		private IHttpContextAccessor accessor;
		private IOptionsSnapshot<Settings> settings;

		/// <summary>
		/// Initializes a new instance of the UsersController class.
		/// </summary>
		/// <param name="manager">
		/// The manager that this instance will use.
		/// </param>
		/// <param name="accessor">
		/// The HTTP context accessor that this instance will use.
		/// </param>
		/// <param name="settings">
		/// The application settings that this instance will use.
		/// </param>
		public UsersController(
			UserManager<User> manager,
			IHttpContextAccessor accessor,
			IOptionsSnapshot<Settings> settings)
		{
			this.manager = manager;
			this.accessor = accessor;
			this.settings = settings;
		}

		/// <summary>
		/// Gets all users from the database.
		/// </summary>
		/// <returns>All users from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<UserDto>))]
		public IEnumerable<UserDto> GetAll()
			=> this.manager.Users.ProjectTo<UserDto>();

		/// <summary>
		/// Gets a user with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the user to get.</param>
		/// <returns>A user with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(UserDto))]
		public async Task<UserDto> GetById([FromRoute] string id)
		{
			var user = await this.manager.FindByIdAsync(id);
			user.RoleNames = await this.manager.GetRolesAsync(user);

			return Mapper.Map<UserDto>(user);
		}

		/// <summary>
		/// Gets a user with the specified email.
		/// </summary>
		/// <param name="email">The email of the user to get.</param>
		/// <returns>A user with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(200, Type = typeof(UserDto))]
		public async Task<UserDto> GetByEmail([FromRoute] string email)
		{
			var user = await this.manager.FindByEmailAsync(email);
			user.RoleNames = await this.manager.GetRolesAsync(user);

			return Mapper.Map<UserDto>(user);
		}

		/// <summary>
		/// Gets the currently logged in user.
		/// </summary>
		/// <returns>The currently logged in user.</returns>
		[HttpGet("current")]
		[SwaggerResponse(200, Type = typeof(UserDto))]
		public async Task<UserDto> GetCurrent()
		{
			string name = this.accessor.HttpContext.User.FindFirst(
				ClaimTypes.NameIdentifier)?.Value;

			var user = await this.manager.FindByNameAsync(name);
			user.RoleNames = await this.manager.GetRolesAsync(user);

			return Mapper.Map<UserDto>(user);
		}

		/// <summary>
		/// Gets the email domain of the users of this application.
		/// </summary>
		/// <returns>
		/// The email domain of the users of this application.
		/// </returns>
		[AllowAnonymous]
		[HttpGet("email-domain")]
		[Produces("text/plain")]
		[SwaggerResponse(200, Type = typeof(string))]
		public string GetEmailDomain()
			=> this.settings.Value.EmailDomain;
	}
}
