using System.Collections.Generic;
using System.Linq;

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
	/// An API for students.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<StudentDto>))]
		public IEnumerable<StudentDto> Get()
			=> this.students.GetAll()?.ProjectTo<StudentDto>();

		/// <summary>
		/// Gets a student with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the student to get.</param>
		/// <returns>A student with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto Get(int id)
			=> Mapper.Map<StudentDto>(this.students.GetById(id));

		/// <summary>
		/// Gets a student with the specified user ID.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>A student with the specified user ID.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByUser(string id)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							?.FirstOrDefault(s => s.UserId == id));

		/// <summary>
		/// Gets a student with the specified email.
		/// </summary>
		/// <param name="email">The email of the student.</param>
		/// <returns>A student with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByEmail(string email)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll()
							?.FirstOrDefault(s => s.User.Email == email));

		/// <summary>
		/// Gets a student with the specified transcript number.
		/// </summary>
		/// <param name="number">The transcript number.</param>
		/// <returns>A student with the specified transcript number.</returns>
		[HttpGet("transcript/{number}")]
		[SwaggerResponse(200, Type = typeof(StudentDto))]
		public StudentDto GetByTranscript(string number)
			=> Mapper.Map<StudentDto>(
				this.students.GetAll().FirstOrDefault(
					s => s.TranscriptNumber == number));

		/// <summary>
		/// Gets all students with the specified group.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <returns>All students with the specified group.</returns>
		[HttpGet("groupId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<StudentDto>))]
		public IEnumerable<StudentDto> GetForGroup(int id)
			=> this.students.GetAll()
						   ?.Where(s => s.GroupId == id)
							.ProjectTo<StudentDto>();
	}
}
