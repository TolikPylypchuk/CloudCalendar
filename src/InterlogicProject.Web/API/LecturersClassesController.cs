using System;
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
	/// An API for lecturer-class relationships.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> Get()
			=> this.lecturersClasses.GetAll()?.ProjectTo<LecturerClassDto>();

		/// <summary>
		/// Gets a lecturer-class relationship with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the relationship to get.</param>
		/// <returns>A relationship with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(200, Type = typeof(LecturerClassDto))]
		public LecturerClassDto Get(int id)
			=> Mapper.Map<LecturerClassDto>(this.lecturersClasses.GetById(id));

		/// <summary>
		/// Gets all lecturer-class relationships with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All relationships with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> GetForClass(int id)
			=> this.lecturersClasses.GetAll()
								   ?.Where(lc => lc.ClassId == id)
									.ProjectTo<LecturerClassDto>();

		/// <summary>
		/// Gets all lecturer-class relationships with the specified lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer.</param>
		/// <returns>All relationships with the specified lecturer.</returns>
		[HttpGet("lecturerId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> GetForLecturer(int id)
			=> this.lecturersClasses.GetAll()
								   ?.Where(lc => lc.LecturerId == id)
									.ProjectTo<LecturerClassDto>();

		/// <summary>
		/// Gets all lecturer-class relationships with the specified lecturer
		/// between the specified dates.
		/// </summary>
		/// <param name="id">The ID of the lecturer.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All relationships with the specified lecturer
		/// between the specified dates.
		/// </returns>
		[HttpGet("lecturerId/{id}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> GetForLecturerWithRange(
			int id,
			DateTime start,
			DateTime end)
			=> this.lecturersClasses.GetAll()
								   ?.Where(lc => lc.LecturerId == id &&
												 lc.Class.DateTime >= start &&
												 lc.Class.DateTime <= end)
									.ProjectTo<LecturerClassDto>();
	}
}
