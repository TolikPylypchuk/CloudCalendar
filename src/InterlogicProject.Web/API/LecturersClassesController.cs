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
	/// An API for lecturer-class relationships.
	/// </summary>
	[Route("api/[controller]")]
	public class LecturersClassesController : Controller
	{
		private IRepository<LecturerClass> lecturersClasses;

		/// <summary>
		/// Initializes a new instance of the LecturersClassesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public LecturersClassesController(IRepository<LecturerClass> repo)
		{
			this.lecturersClasses = repo;
		}

		/// <summary>
		/// Gets all lecturer-class relationships from the database.
		/// </summary>
		/// <returns>
		/// All lecturer-class relationships from the database.
		/// </returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> Get()
			=> this.lecturersClasses.GetAll().ProjectTo<LecturerClassDto>();

		/// <summary>
		/// Gets a lecturer-class relationship with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the relationship to get.</param>
		/// <returns>A relationship with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ClassDto))]
		public LecturerClassDto Get(int id)
			=> Mapper.Map<LecturerClassDto>(this.lecturersClasses.GetById(id));

		/// <summary>
		/// Gets all lecturer-class relationships with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All relationships with the specified class.</returns>
		[HttpGet("classId/{id}")]
		public IEnumerable<LecturerClassDto> GetByClass(int id)
			=> this.lecturersClasses.GetAll()
									.Where(lc => lc.ClassId == id)
									.ProjectTo<LecturerClassDto>();

		/// <summary>
		/// Gets all lecturer-class relationships with the specified lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer.</param>
		/// <returns>All relationships with the specified lecturer.</returns>
		[HttpGet("lecturerId/{id}")]
		public IEnumerable<LecturerClassDto> GetByLecturer(int id)
			=> this.lecturersClasses.GetAll()
									.Where(lc => lc.LecturerId == id)
									.ProjectTo<LecturerClassDto>();
	}
}
