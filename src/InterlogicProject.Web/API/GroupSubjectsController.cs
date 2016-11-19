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
	/// An API for group subjects.
	/// </summary>
	[Route("api/[controller]")]
	public class GroupSubjectsController : Controller
	{
		private IRepository<GroupSubject> subjects;

		/// <summary>
		/// Initializes a new instance of the GroupSubjectsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public GroupSubjectsController(IRepository<GroupSubject> repo)
		{
			this.subjects = repo;
		}

		/// <summary>
		/// Gets all group subjects from the database.
		/// </summary>
		/// <returns>All group subjects from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupSubjectDto>))]
		public IEnumerable<GroupSubjectDto> Get()
			=> this.subjects.GetAll().ProjectTo<GroupSubjectDto>();

		/// <summary>
		/// Gets a group subject with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the group subject to get.</param>
		/// <returns>A group subject with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(GroupSubjectDto))]
		public GroupSubjectDto Get(int id)
			=> Mapper.Map<GroupSubjectDto>(this.subjects.GetById(id));
	}
}
