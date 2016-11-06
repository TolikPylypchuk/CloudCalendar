using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Models.Dto;

namespace InterlogicProject.API
{
	/// <summary>
	/// An API for groups.
	/// </summary>
	[Route("api/[controller]")]
	public class GroupsController : Controller
	{
		private IRepository<Group> groups;

		/// <summary>
		/// Initializes a new instance of the GroupsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public GroupsController(IRepository<Group> repo)
		{
			this.groups = repo;
		}

		/// <summary>
		/// Gets all groups from the database.
		/// </summary>
		/// <returns>All groups from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> Get()
			=> this.groups.GetAll().ProjectTo<GroupDto>();

		/// <summary>
		/// Gets a group with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the group to get.</param>
		/// <returns>A group with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(GroupDto))]
		public GroupDto Get(int id)
			=> Mapper.Map<GroupDto>(this.groups.GetById(id));
	}
}
