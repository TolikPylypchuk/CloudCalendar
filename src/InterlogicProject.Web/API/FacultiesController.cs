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
	/// An API for faculties.
	/// </summary>
	[Route("api/[controller]")]
	public class FacultiesController : Controller
	{
		private IRepository<Faculty> faculties;

		/// <summary>
		/// Initializes a new instance of the FacultiesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public FacultiesController(IRepository<Faculty> repo)
		{
			this.faculties = repo;
		}

		/// <summary>
		/// Gets all faculties from the database.
		/// </summary>
		/// <returns>All faculties from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<FacultyDto>))]
		public IEnumerable<FacultyDto> Get()
			=> this.faculties.GetAll().ProjectTo<FacultyDto>();

		/// <summary>
		/// Gets a faculty with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the faculty to get.</param>
		/// <returns>A faculty with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(FacultyDto))]
		public FacultyDto Get(int id)
			=> Mapper.Map<FacultyDto>(this.faculties.GetById(id));
	}
}
