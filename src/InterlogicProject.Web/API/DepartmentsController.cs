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
			Type = typeof(IEnumerable<DepartmentDto>))]
		public IEnumerable<DepartmentDto> Get()
			=> this.departments.GetAll()?.ProjectTo<DepartmentDto>();

		/// <summary>
		/// Gets a department with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the department to get.</param>
		/// <returns>A department with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(DepartmentDto))]
		public DepartmentDto Get(int id)
			=> Mapper.Map<DepartmentDto>(this.departments.GetById(id));

		/// <summary>
		/// Gets all departments with the specified faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <returns>All departments with the specified faculty.</returns>
		[HttpGet("facultyId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<DepartmentDto>))]
		public IEnumerable<DepartmentDto> GetByFaculty(int id)
			=> this.departments.GetAll()
							  ?.Where(d => d.FacultyId == id)
							   .ProjectTo<DepartmentDto>();
	}
}
