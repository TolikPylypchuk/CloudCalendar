using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

using Swashbuckle.SwaggerGen.Annotations;

namespace InterlogicProject.API
{
	/// <summary>
	/// An API for departments.
	/// </summary>
	[Route("api/[controller]")]
	public class DepartmentsController : Controller
	{
		private IRepository<Department> departments;

		/// <summary>
		/// Initializes a new instance of the DepartmentsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public DepartmentsController(IRepository<Department> repo)
		{
			this.departments = repo;
		}

		/// <summary>
		/// Gets all departments from the database.
		/// </summary>
		/// <returns>All departments from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<Department>))]
		public IEnumerable<Department> Get()
			=> this.departments.GetAll();

		/// <summary>
		/// Gets a department with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the department to get.</param>
		/// <returns>A department with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Department))]
		public Department Get(int id)
			=> this.departments.GetById(id);
	}
}
