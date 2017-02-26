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
	/// An API for class places.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ClassPlacesController : Controller
	{
		private IRepository<ClassPlace> places;

		/// <summary>
		/// Initializes a new instance of the ClassPlacesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public ClassPlacesController(IRepository<ClassPlace> repo)
		{
			this.places = repo;
		}

		/// <summary>
		/// Gets all class places from the database.
		/// </summary>
		/// <returns>All class places from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200,
			Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> Get()
			=> this.places.GetAll()?.ProjectTo<ClassPlaceDto>();

		/// <summary>
		/// Gets a class place with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the class place to get.</param>
		/// <returns>A class place with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(200, Type = typeof(ClassPlaceDto))]
		public ClassPlaceDto Get(int id)
			=> Mapper.Map<ClassPlaceDto>(this.places.GetById(id));

		/// <summary>
		/// Gets all class places with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All class places with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> GetForClass(int id)
			=> this.places.GetAll()
						 ?.Where(p => p.ClassId == id)
						  .ProjectTo<ClassPlaceDto>();

		/// <summary>
		/// Gets all class places with the specified classroom.
		/// </summary>
		/// <param name="id">The ID of the classroom.</param>
		/// <returns>All class places with the specified classroom.</returns>
		[HttpGet("classroomId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> GetForClassroom(int id)
			=> this.places.GetAll()
						 ?.Where(p => p.ClassroomId == id)
						  .ProjectTo<ClassPlaceDto>();

		/// <summary>
		/// Gets all class places with the specified classroom
		/// beetween the specified dates.
		/// </summary>
		/// <param name="id">The ID of the classroom.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All class places with the specified classroom
		/// beetween the specified dates.
		/// </returns>
		[HttpGet("classroomId/{id}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassPlaceDto>))]
		public IEnumerable<ClassPlaceDto> GetForClassroom(
			int id,
			DateTime start,
			DateTime end)
			=> this.places.GetAll()
						 ?.Where(p => p.ClassroomId == id &&
									  p.Class.DateTime >= start &&
									  p.Class.DateTime <= end)
						  .ProjectTo<ClassPlaceDto>();
	}
}
