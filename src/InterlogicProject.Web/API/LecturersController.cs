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
	/// An API for lecturers.
	/// </summary>
	[Route("api/[controller]")]
	public class LecturersController : Controller
	{
		private IRepository<Lecturer> groups;

		/// <summary>
		/// Initializes a new instance of the LecturersController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public LecturersController(IRepository<Lecturer> repo)
		{
			this.groups = repo;
		}

		/// <summary>
		/// Gets all lecturers from the database.
		/// </summary>
		/// <returns>All lecturers from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> Get()
			=> this.groups.GetAll().ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets a lecturer with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the lecturer to get.</param>
		/// <returns>A lecturer with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LecturerDto))]
		public LecturerDto Get(int id)
			=> Mapper.Map<LecturerDto>(this.groups.GetById(id));
	}
}
