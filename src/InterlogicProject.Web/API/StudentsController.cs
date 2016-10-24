using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject.API
{
	/// <summary>
	/// An API for students.
	/// </summary>
	[Route("api/[controller]")]
	public class StudentsController : Controller
	{
		private IRepository<Student> students;

		/// <summary>
		/// Initializes a new instance of the StudentsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public StudentsController(IRepository<Student> repo)
		{
			this.students = repo;
		}

		/// <summary>
		/// Gets all students from the database.
		/// </summary>
		/// <returns>All students from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<Student>))]
		public IEnumerable<Student> Get()
			=> this.students.GetAll();

		/// <summary>
		/// Gets a student with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the student to get.</param>
		/// <returns>A student with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Student))]
		public Student Get(int id)
			=> this.students.GetById(id);
	}
}
