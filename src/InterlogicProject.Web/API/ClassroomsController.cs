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
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for classrooms.
	/// </summary>
	[Route("api/[controller]")]
	public class ClassroomsController : Controller
	{
		private IRepository<Classroom> classrooms;

		/// <summary>
		/// Initializes a new instance of the ClassroomsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public ClassroomsController(IRepository<Classroom> repo)
		{
			this.classrooms = repo;
		}

		/// <summary>
		/// Gets all classrooms from the database.
		/// </summary>
		/// <returns>All classrooms from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> Get()
			=> this.classrooms.GetAll()?.ProjectTo<ClassroomDto>();

		/// <summary>
		/// Gets a classroom with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the classroom to get.</param>
		/// <returns>A classroom with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ClassroomDto))]
		public ClassroomDto Get(int id)
			=> Mapper.Map<ClassroomDto>(this.classrooms.GetById(id));

		/// <summary>
		/// Gets all classrooms with the specified building.
		/// </summary>
		/// <returns>All classrooms with the specified building.</returns>
		[HttpGet("buildingId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> GetForBuilding(int id)
			=> this.classrooms.GetAll()
							 ?.Where(c => c.BuildingId == id)
							  .ProjectTo<ClassroomDto>();

		/// <summary>
		/// Gets all classrooms with the specified class.
		/// </summary>
		/// <returns>All classrooms with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> GetForClass(int id)
			=> this.classrooms.GetAll()
							 ?.Include(r => r.Classes)
							  .Where(r => r.Classes.Any(c => c.ClassId == id))
							  .ProjectTo<ClassroomDto>();
	}
}
