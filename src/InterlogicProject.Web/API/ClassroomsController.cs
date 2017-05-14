using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for classrooms.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
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
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> GetAll()
			=> this.classrooms.GetAll()?.ProjectTo<ClassroomDto>();

		/// <summary>
		/// Gets a classroom with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the classroom to get.</param>
		/// <returns>A classroom with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(ClassroomDto))]
		public ClassroomDto GetById([FromRoute] int id)
			=> Mapper.Map<ClassroomDto>(this.classrooms.GetById(id));

		/// <summary>
		/// Gets all classrooms with the specified building.
		/// </summary>
		/// <returns>All classrooms with the specified building.</returns>
		[HttpGet("buildingId/{buildingId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> GetForBuilding(
			[FromRoute] int buildingId)
			=> this.classrooms.GetAll()
							 ?.Where(c => c.BuildingId == buildingId)
							  .ProjectTo<ClassroomDto>();

		/// <summary>
		/// Gets all classrooms with the specified class.
		/// </summary>
		/// <returns>All classrooms with the specified class.</returns>
		[HttpGet("classId/{classId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassroomDto>))]
		public IEnumerable<ClassroomDto> GetForClass([FromRoute] int classId)
			=> this.classrooms.GetAll()
							 ?.Include(r => r.Classes)
							  .Where(r => r.Classes.Any(
								  c => c.ClassId == classId))
							  .ProjectTo<ClassroomDto>();

		/// <summary>
		/// Adds a new classroom to the database.
		/// </summary>
		/// <param name="classroomDto">The classroom to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] ClassroomDto classroomDto)
		{
			if (classroomDto?.Name == null ||
				classroomDto.BuildingId == 0)
			{
				return this.BadRequest();
			}

			var classroomToAdd = new Classroom
			{
				Name = classroomDto.Name,
				BuildingId = classroomDto.BuildingId
			};

			this.classrooms.Add(classroomToAdd);

			classroomDto.Id = classroomToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById),
				new { id = classroomDto.Id },
				classroomDto);
		}

		/// <summary>
		/// Updates a classroom.
		/// </summary>
		/// <param name="id">The ID of the classroom to update.</param>
		/// <param name="classroomDto">The classroom to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] ClassroomDto classroomDto)
		{
			if (classroomDto?.Name == null ||
				classroomDto.BuildingId == 0)
			{
				return this.BadRequest();
			}

			var classroomToUpdate = this.classrooms.GetById(id);

			if (classroomToUpdate == null)
			{
				return this.NotFound();
			}

			classroomToUpdate.Name = classroomDto.Name;
			this.classrooms.Update(classroomToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a classroom.
		/// </summary>
		/// <param name="id">The ID of the classroom to update.</param>
		/// <param name="classroomDto">The classroom to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] ClassroomDto classroomDto)
		{
			if (classroomDto?.Name == null ||
				classroomDto.BuildingId == 0)
			{
				return this.BadRequest();
			}

			var classroomToUpdate = this.classrooms.GetById(id);

			if (classroomToUpdate == null)
			{
				return this.NotFound();
			}

			classroomToUpdate.Name = classroomDto.Name;
			this.classrooms.Update(classroomToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a classroom.
		/// </summary>
		/// <param name="id">The ID of the classroom to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromRoute] int id)
		{
			var classroomToDelete = this.classrooms.GetById(id);

			if (classroomToDelete == null)
			{
				return this.NotFound();
			}

			this.classrooms.Delete(classroomToDelete);

			return this.NoContent();
		}
	}
}
