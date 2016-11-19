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
	/// An API for subjects.
	/// </summary>
    [Route("api/[controller]")]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<SubjectDto>))]
		public IEnumerable<SubjectDto> Get()
			=> this.subjects.GetAll().ProjectTo<SubjectDto>();

		/// <summary>
		/// Gets a subject with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the subject to get.</param>
		/// <returns>A subject with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(SubjectDto))]
		public SubjectDto Get(int id)
			=> Mapper.Map<SubjectDto>(this.subjects.GetById(id));
    }
}
