using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for lecturers.
	/// </summary>
	[Route("api/[controller]")]
	public class LecturersController : Controller
	{
		private IRepository<Lecturer> lecturers;

		/// <summary>
		/// Initializes a new instance of the LecturersController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public LecturersController(IRepository<Lecturer> repo)
		{
			this.lecturers = repo;
		}

		/// <summary>
		/// Gets all lecturers from the database.
		/// </summary>
		/// <returns>All lecturers from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> Get()
			=> this.lecturers.GetAll()?.ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets a lecturer with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the lecturer to get.</param>
		/// <returns>A lecturer with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LecturerDto))]
		public LecturerDto Get(int id)
			=> Mapper.Map<LecturerDto>(this.lecturers.GetById(id));

		/// <summary>
		/// Gets a lecturer with the specified user ID.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>A lecturer with the specified user ID.</returns>
		[HttpGet("userId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LecturerDto))]
		public LecturerDto GetByUser(string id)
			=> Mapper.Map<LecturerDto>(
				this.lecturers.GetAll()
							 ?.FirstOrDefault(l => l.UserId == id));

		/// <summary>
		/// Gets a lecturer with the specified email.
		/// </summary>
		/// <param name="email">The email of the lecturer.</param>
		/// <returns>A lecturer with the specified email.</returns>
		[HttpGet("email/{email}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LecturerDto))]
		public LecturerDto GetByEmail(string email)
			=> Mapper.Map<LecturerDto>(
				this.lecturers.GetAll()
							 ?.FirstOrDefault(l => l.User.Email == email));

		/// <summary>
		/// Gets all lecturers with the specified department.
		/// </summary>
		/// <param name="id">The ID of the department.</param>
		/// <returns>All lecturers with the specified department.</returns>
		[HttpGet("departmentId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> GetForDepartment(int id)
			=> this.lecturers.GetAll()
							?.Where(l => l.DepartmentId == id)
							 .ProjectTo<LecturerDto>();

		/// <summary>
		/// Gets all lecturers with the specified faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <returns>All lecturers with the specified faculty.</returns>
		[HttpGet("faculty/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerDto>))]
		public IEnumerable<LecturerDto> GetForFaculty(int id)
			=> this.lecturers.GetAll()
							?.Where(l => l.Department.FacultyId == id)
							 .ProjectTo<LecturerDto>();
	}
}
