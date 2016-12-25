using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Models.Dto;

namespace InterlogicProject.API
{
	/// <summary>
	/// An API for classes.
	/// </summary>
	[Route("api/[controller]")]
	public class ClassesController : Controller
	{
		private IRepository<Class> classes;

		/// <summary>
		/// Initializes a new instance of the ClassesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public ClassesController(IRepository<Class> repo)
		{
			this.classes = repo;
		}

		/// <summary>
		/// Gets all classes from the database.
		/// </summary>
		/// <returns>All classes from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> Get()
			=> this.classes.GetAll()?.ProjectTo<ClassDto>();

		/// <summary>
		/// Gets a class with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the class to get.</param>
		/// <returns>A class with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ClassDto))]
		public ClassDto Get(int id)
			=> Mapper.Map<ClassDto>(this.classes.GetById(id));

		/// <summary>
		/// Gets all classes between the specified dates.
		/// </summary>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>All classes between the specified dates.</returns>
		[HttpGet("range/{start}/{end}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetWithRange(DateTime start, DateTime end)
			=> this.classes.GetAll()
						  ?.Where(c => c.DateTime >= start.Date &&
									   c.DateTime <= end.Date)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified group.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <returns>All classes of the specified group.</returns>
		[HttpGet("groupId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroup(int id)
			=> this.classes.GetAll()
						  ?.Where(c => c.GroupId == id)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified group
		/// between the specified dates.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified group
		/// between the specified dates.
		/// </returns>
		[HttpGet("groupId/{id}/range/{start}/{end}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroupWithRange(
			int id,
			DateTime start,
			DateTime end)
			=> this.classes.GetAll()
						  ?.Where(c => c.GroupId == id &&
									   c.DateTime >= start.Date &&
									   c.DateTime <= end.Date)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified lecturer.
		/// </summary>
		/// <param name="id">The ID of the lecturer.</param>
		/// <returns>All classes of the specified lecturer.</returns>
		[HttpGet("lecturerId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForLecturer(int id)
			=> this.classes.GetAll()
						   .Include(c => c.Lecturers)
						  ?.Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == id))
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified lecturer
		/// between the specified dates.
		/// </summary>
		/// <param name="id">The ID of the lecturer.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified lecturer.
		/// between the specified dates.
		/// </returns>
		[HttpGet("lecturerId/{id}/range/{start}/{end}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForLecturerWithRange(
			int id,
			DateTime start,
			DateTime end)
			=> this.classes.GetAll()
						   .Where(c => c.DateTime >= start.Date &&
									   c.DateTime <= end.Date)
						   .Include(c => c.Lecturers)
						  ?.Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == id))
						   .ProjectTo<ClassDto>();
	}
}
