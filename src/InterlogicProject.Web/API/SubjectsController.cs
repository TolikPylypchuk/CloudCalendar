using System.Collections.Generic;

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
	/// An API for subjects.
	/// </summary>
    [Route("api/[controller]")]
	[Produces("application/json")]
	public class SubjectsController : Controller
    {
		private IRepository<Subject> subjects;

		/// <summary>
		/// Initializes a new instance of the SubjectsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public SubjectsController(IRepository<Subject> repo)
		{
			this.subjects = repo;
		}

		/// <summary>
		/// Gets all subjects from the database.
		/// </summary>
		/// <returns>All subjects from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<SubjectDto>))]
		public IEnumerable<SubjectDto> Get()
			=> this.subjects.GetAll()?.ProjectTo<SubjectDto>();

		/// <summary>
		/// Gets a subject with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the subject to get.</param>
		/// <returns>A subject with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(200, Type = typeof(SubjectDto))]
		public SubjectDto Get(int id)
			=> Mapper.Map<SubjectDto>(this.subjects.GetById(id));
    }
}
