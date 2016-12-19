using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

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
			Type = typeof(IEnumerable<Faculty>))]
		public IEnumerable<Faculty> Get()
			=> this.faculties.GetAll().ProjectTo<Faculty>();

		/// <summary>
		/// Gets a faculty with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the faculty to get.</param>
		/// <returns>A faculty with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Faculty))]
		public Faculty Get(int id)
			=> Mapper.Map<Faculty>(this.faculties.GetById(id));
	}
}
