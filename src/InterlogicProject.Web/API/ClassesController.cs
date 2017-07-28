using System;
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
	/// An API for classes.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ClassesController : Controller
	{
		private IRepository<Class> classes;
		private IRepository<ClassPlace> classPlaces;
		private IRepository<GroupClass> groupClasses;
		private IRepository<LecturerClass> lecturerClasses;

		/// <summary>
		/// Initializes a new instance of the ClassesController class.
		/// </summary>
		/// <param name="classes">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="classPlaces">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="groupClasses">
		/// The repository that this instance will use.
		/// </param>
		/// <param name="lecturerClasses">
		/// The repository that this instance will use.
		/// </param>
		public ClassesController(
			IRepository<Class> classes,
			IRepository<ClassPlace> classPlaces,
			IRepository<GroupClass> groupClasses,
			IRepository<LecturerClass> lecturerClasses)
		{
			this.classes = classes;
			this.classPlaces = classPlaces;
			this.groupClasses = groupClasses;
			this.lecturerClasses = lecturerClasses;
		}

		/// <summary>
		/// Gets all classes from the database.
		/// </summary>
		/// <returns>All classes from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetAll()
			=> this.classes.GetAll()?.ProjectTo<ClassDto>();

		/// <summary>
		/// Gets a class with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the class to get.</param>
		/// <returns>A class with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(ClassDto))]
		public ClassDto GetById([FromRoute] int id)
			=> Mapper.Map<ClassDto>(this.classes.GetById(id));

		/// <summary>
		/// Gets all classes between the specified dates.
		/// </summary>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>All classes between the specified dates.</returns>
		[HttpGet("range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetWithRange(
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.classes.GetAll()
						  ?.Where(c => c.DateTime >= start &&
									   c.DateTime <= end)
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified group.
		/// </summary>
		/// <param name="groupId">The ID of the group.</param>
		/// <returns>All classes of the specified group.</returns>
		[HttpGet("groupId/{groupId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroup([FromRoute] int groupId)
			=> this.classes.GetAll()
						  ?.Where(c => c.Groups.Any(g => g.GroupId == groupId))
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified group
		/// between the specified dates.
		/// </summary>
		/// <param name="groupId">The ID of the group.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified group
		/// between the specified dates.
		/// </returns>
		[HttpGet("groupId/{groupId}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroupWithRange(
			[FromRoute] int groupId,
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.classes.GetAll()
						  ?.Where(c => c.Groups.Any(g => g.GroupId == groupId))
						   .Where(c => c.DateTime >= start &&
									   c.DateTime <= end)
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets a class for the specified group
		/// with the specified date and number.
		/// </summary>
		/// <param name="groupId">The ID of the group.</param>
		/// <param name="dateTime">The date of the class.</param>
		/// <returns>
		/// A class for the specified group with the specified date and number.
		/// </returns>
		[HttpGet("groupId/{groupId}/dateTime/{dateTime}")]
		[SwaggerResponse(200, Type = typeof(ClassDto))]
		public ClassDto GetForGroupByDateTime(
			[FromRoute] int groupId,
			[FromRoute] DateTime dateTime)
			=> this.classes.GetAll()
						   .Where(c => c.Groups.Any(gc => gc.GroupId == groupId))
						   .Where(c => c.DateTime == dateTime)
						   .ProjectTo<ClassDto>()
						   .FirstOrDefault();

		/// <summary>
		/// Gets all classes of the specified lecturer.
		/// </summary>
		/// <param name="lecturerId">The ID of the lecturer.</param>
		/// <returns>All classes of the specified lecturer.</returns>
		[HttpGet("lecturerId/{lecturerId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForLecturer([FromRoute] int lecturerId)
			=> this.classes.GetAll()
						  ?.Include(c => c.Lecturers)
						   .Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == lecturerId))
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified lecturer
		/// between the specified dates.
		/// </summary>
		/// <param name="lecturerId">The ID of the lecturer.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified lecturer.
		/// between the specified dates.
		/// </returns>
		[HttpGet("lecturerId/{lecturerId}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForLecturerWithRange(
			[FromRoute] int lecturerId,
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.classes.GetAll()
						  ?.Where(c => c.DateTime >= start &&
									   c.DateTime <= end)
						   .Include(c => c.Lecturers)
						   .Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == lecturerId))
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets a class for the specified lecturer
		/// with the specified date and number.
		/// </summary>
		/// <param name="lecturerId">The ID of the lecturer.</param>
		/// <param name="dateTime">The date of the class.</param>
		/// <returns>
		/// A class for the specified lecturer with the specified date and number.
		/// </returns>
		[HttpGet("lecturerId/{lecturerId}/dateTime/{dateTime}")]
		[SwaggerResponse(200, Type = typeof(ClassDto))]
		public ClassDto GetForLecturerByDateTime(
			[FromRoute] int lecturerId,
			[FromRoute] DateTime dateTime)
			=> this.classes.GetAll()
						   .Where(c => c.Lecturers.Any(
								ls => ls.LecturerId == lecturerId))
						   .Where(c => c.DateTime == dateTime)
						   .ProjectTo<ClassDto>()
						   .FirstOrDefault();

		/// <summary>
		/// Gets all classes of the specified group and lecturer.
		/// </summary>
		/// <param name="groupId">The ID of the group.</param>
		/// <param name="lecturerId">The ID of the lecturer.</param>
		/// <returns>All classes of the specified group and lecturer.</returns>
		[HttpGet("groupId/{groupId}/lecturerId/{lecturerId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroupAndLecturer(
			[FromRoute] int groupId,
			[FromRoute] int lecturerId)
			=> this.classes.GetAll()
						  ?.Include(c => c.Lecturers)
						   .Where(c => c.Groups.Any(g => g.GroupId == groupId))
						   .Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == lecturerId))
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified group and lecturer
		/// between the specified dates.
		/// </summary>
		/// <param name="groupId">The ID of the group.</param>
		/// <param name="lecturerId">The ID of the lecturer.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified group and lecturer
		/// between the specified dates.
		/// </returns>
		[HttpGet(
			"groupId/{groupId}/lecturerId/{lecturerId}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForGroupAndLecturerWithRange(
			[FromRoute] int groupId,
			[FromRoute] int lecturerId,
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.classes.GetAll()
						  ?.Include(c => c.Lecturers)
						   .Where(c => c.Groups.Any(g => g.GroupId == groupId))
						   .Where(c => c.Lecturers.Any(
									lc => lc.LecturerId == lecturerId))
						   .Where(c => c.DateTime >= start &&
									   c.DateTime <= end)
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified classroom.
		/// </summary>
		/// <param name="classroomId">The ID of the classroom.</param>
		/// <returns>All classes of the specified classroom.</returns>
		[HttpGet("classroomId/{classroomId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForClassroom(
			[FromRoute] int classroomId)
			=> this.classes.GetAll()
						  ?.Include(c => c.Places)
						   .Where(c => c.Places.Any(
							   p => p.ClassroomId == classroomId))
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Gets all classes of the specified classroom
		/// between the specified dates.
		/// </summary>
		/// <param name="classroomId">The ID of the classroom.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All classes of the specified classroom
		/// between the specified dates.
		/// </returns>
		[HttpGet("classroomId/{classroomId}/range/{start}/{end}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<ClassDto>))]
		public IEnumerable<ClassDto> GetForClassroomWithRange(
			[FromRoute] int classroomId,
			[FromRoute] DateTime start,
			[FromRoute] DateTime end)
			=> this.classes.GetAll()
						  ?.Include(c => c.Places)
						   .Where(c => c.Places.Any(
							   p => p.ClassroomId == classroomId))
						   .Where(c => c.DateTime >= start &&
									   c.DateTime <= end)
						   .OrderBy(c => c.DateTime)
						   .ProjectTo<ClassDto>();

		/// <summary>
		/// Adds a new class to the database.
		/// </summary>
		/// <param name="classDto">The class to add.</param>
		/// <param name="groupIds">
		/// The IDs of the groups of the class.
		/// </param>
		/// <param name="lecturerIds">
		/// The IDs of the lecturers of the class.
		/// </param>
		/// <param name="classroomIds">
		/// The IDs of the classrooms of the class.
		/// </param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Admin,Lecturer")]
		public IActionResult Post(
			[FromBody] ClassDto classDto,
			[FromQuery] int[] classroomIds,
			[FromQuery] int[] groupIds,
			[FromQuery] int[] lecturerIds)
		{
			if (classDto?.Type == null ||
				classDto.DateTime == default(DateTime) ||
				classDto.SubjectId == 0 || groupIds == null ||
				lecturerIds == null || classroomIds == null ||
				groupIds.Length == 0 || lecturerIds.Length == 0 ||
				classroomIds.Length == 0)
			{
				return this.BadRequest();
			}

			var classToAdd = new Class
			{
				Type = classDto.Type,
				SubjectId = classDto.SubjectId,
				DateTime = classDto.DateTime,
				HomeworkEnabled = classDto.HomeworkEnabled
			};

			this.classes.Add(classToAdd);

			this.classPlaces.AddRange(
				classroomIds.Select(
					id => new ClassPlace
					{
						ClassId = classToAdd.Id,
						ClassroomId = id
					}));

			this.groupClasses.AddRange(
				groupIds.Select(
					id => new GroupClass
					{
						ClassId = classToAdd.Id,
						GroupId = id
					}));

			this.lecturerClasses.AddRange(
				lecturerIds.Select(
					id => new LecturerClass
					{
						ClassId = classToAdd.Id,
						LecturerId = id
					}));

			classDto.Id = classToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById), new { id = classDto.Id }, classDto);
		}

		/// <summary>
		/// Updates a class.
		/// </summary>
		/// <param name="id">The ID of the class to update.</param>
		/// <param name="classDto">The class to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin,Lecturer")]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] ClassDto classDto)
		{
			if (classDto?.Type == null ||
				classDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var classToUpdate = this.classes.GetById(id);

			if (classToUpdate == null)
			{
				return this.NotFound();
			}

			classToUpdate.Type = classDto.Type;
			classToUpdate.DateTime = classDto.DateTime;
			classToUpdate.HomeworkEnabled = classDto.HomeworkEnabled;
			this.classes.Update(classToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a class.
		/// </summary>
		/// <param name="id">The ID of the class to update.</param>
		/// <param name="classDto">The class to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin,Lecturer")]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] ClassDto classDto)
		{
			if (classDto?.Type == null ||
				classDto.DateTime == default(DateTime))
			{
				return this.BadRequest();
			}

			var classToUpdate = this.classes.GetById(id);

			if (classToUpdate == null)
			{
				return this.NotFound();
			}

			classToUpdate.Type = classDto.Type;
			classToUpdate.DateTime = classDto.DateTime;
			classToUpdate.HomeworkEnabled = classDto.HomeworkEnabled;
			this.classes.Update(classToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a class.
		/// </summary>
		/// <param name="id">The ID of the class to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromRoute] int id)
		{
			var classToDelete = this.classes.GetById(id);

			if (classToDelete == null)
			{
				return this.NotFound();
			}

			this.classes.Delete(classToDelete);

			return this.NoContent();
		}
	}
}
