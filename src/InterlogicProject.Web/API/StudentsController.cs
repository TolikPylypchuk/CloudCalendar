using System.Collections.Generic;
using System.Linq;
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
			Type = typeof(IEnumerable<StudentDto>))]
		public IEnumerable<StudentDto> Get()
			=> this.students.GetAll().ProjectTo<StudentDto>();

		/// <summary>
		/// Gets a student with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the student to get.</param>
		/// <returns>A student with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(StudentDto))]
		public StudentDto Get(int id)
			=> Mapper.Map<StudentDto>(this.students.GetById(id));

		/// <summary>
		/// Gets a student with the specified user ID.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>A student with the specified user ID.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(StudentDto))]
		public StudentDto GetByUser(string id)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							 .FirstOrDefault(s => s.UserId == id));

		/// <summary>
		/// Gets a student with the specified email.
		/// </summary>
		/// <param name="email">The email of the student.</param>
		/// <returns>A student with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(StudentDto))]
		public StudentDto GetByEmail(string email)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							  .FirstOrDefault(s => s.User.Email == email));
	}
}
