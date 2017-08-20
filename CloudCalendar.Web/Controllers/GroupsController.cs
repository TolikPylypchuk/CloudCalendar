using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.AspNetCore.SwaggerGen;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Web.Models.Dto;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for groups.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class GroupsController : Controller
	{
		private IRepository<Group> groups;

		/// <summary>
		/// Initializes a new instance of the GroupsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public GroupsController(IRepository<Group> repo)
		{
			this.groups = repo;
		}

		/// <summary>
		/// Gets all groups from the database.
		/// </summary>
		/// <returns>All groups from the database.</returns>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetAll()
			=> this.groups.GetAll()?.ProjectTo<GroupDto>();

		/// <summary>
		/// Gets a group with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the group to get.</param>
		/// <returns>A group with the specified ID.</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(GroupDto))]
		public GroupDto GetById([FromRoute] int id)
			=> Mapper.Map<GroupDto>(this.groups.GetById(id));

		/// <summary>
		/// Gets all groups with the specified department.
		/// </summary>
		/// <param name="departmentId">The ID of the department.</param>
		/// <returns>All groups with the specified department.</returns>
		[HttpGet("departmentId/{departmentId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForDepartment(
			[FromRoute] int departmentId)
			=> this.groups.GetAll()
						 ?.Where(g => g.Curator.DepartmentId == departmentId)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified faculty.
		/// </summary>
		/// <param name="facultyId">The ID of the faculty.</param>
		/// <returns>All groups with the specified faculty.</returns>
		[HttpGet("facultyId/{facultyId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForFaculty([FromRoute] int facultyId)
			=> this.groups.GetAll()
						 ?.Where(g =>
								g.Curator.Department.FacultyId == facultyId)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified enrollment year.
		/// </summary>
		/// <param name="year">The enrollment year.</param>
		/// <returns>All groups with the specified  enrollment year.</returns>
		[HttpGet("year/{year}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForYear([FromRoute] int year)
			=> this.groups.GetAll()
						 ?.Where(g => g.Year == year)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified faculty and enrollment year.
		/// </summary>
		/// <param name="facultyId">The ID of the faculty.</param>
		/// <param name="year">The enrollment year.</param>
		/// <returns>
		/// All groups with the specified faculty and enrollment year.
		/// </returns>
		[HttpGet("facultyId/{facultyId}/year/{year}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForFaculty(
			[FromRoute] int facultyId,
			[FromRoute] int year)
			=> this.groups.GetAll()
						 ?.Where(g => g.Year == year &&
									  g.Curator.Department.FacultyId ==
									  facultyId)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified class.
		/// </summary>
		/// <param name="classId">The ID of the class.</param>
		/// <returns>All groups with the specified class.</returns>
		[HttpGet("classId/{classId}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForClass([FromRoute] int classId)
			=> this.groups.GetAll()
						 ?.Include(g => g.Classes)
						  .Where(g => g.Classes.Any(c => c.ClassId == classId))
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Adds a new group to the database.
		/// </summary>
		/// <param name="groupDto">The group to add.</param>
		/// <returns>
		/// The action result that represents the status code 201.
		/// </returns>
		[HttpPost]
		[SwaggerResponse(201)]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] GroupDto groupDto)
		{
			if (groupDto?.Name == null ||
				groupDto.Year == 0 ||
				groupDto.CuratorId == 0)
			{
				return this.BadRequest();
			}

			var groupToAdd = new Group
			{
				Name = groupDto.Name,
				Year = groupDto.Year,
				CuratorId = groupDto.CuratorId
			};

			this.groups.Add(groupToAdd);

			groupDto.Id = groupToAdd.Id;

			return this.CreatedAtAction(
				nameof(this.GetById), new { id = groupDto.Id }, groupDto);
		}

		/// <summary>
		/// Updates a group.
		/// </summary>
		/// <param name="id">The ID of the group to update.</param>
		/// <param name="groupDto">The group to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPut("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] GroupDto groupDto)
		{
			if (groupDto == null)
			{
				return this.BadRequest();
			}

			var groupToUpdate = this.groups.GetById(id);

			if (groupToUpdate == null)
			{
				return this.NotFound();
			}

			if (groupDto.Name != null)
			{
				groupToUpdate.Name = groupDto.Name;
			}
			
			if (groupDto.CuratorId != 0)
			{
				groupToUpdate.CuratorId = groupDto.CuratorId;
			}

			this.groups.Update(groupToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Updates a group.
		/// </summary>
		/// <param name="id">The ID of the group to update.</param>
		/// <param name="groupDto">The group to update.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpPatch("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] GroupDto groupDto)
		{
			if (groupDto == null)
			{
				return this.BadRequest();
			}

			var groupToUpdate = this.groups.GetById(id);

			if (groupToUpdate == null)
			{
				return this.NotFound();
			}

			if (groupDto.Name != null)
			{
				groupToUpdate.Name = groupDto.Name;
			}
			
			if (groupDto.CuratorId != 0)
			{
				groupToUpdate.CuratorId = groupDto.CuratorId;
			}

			this.groups.Update(groupToUpdate);

			return this.NoContent();
		}

		/// <summary>
		/// Deletes a group.
		/// </summary>
		/// <param name="id">The ID of the group to delete.</param>
		/// <returns>
		/// The action result that represents the status code 204.
		/// </returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(204)]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete([FromRoute] int id)
		{
			var groupToDelete = this.groups.GetById(id);

			if (groupToDelete == null)
			{
				return this.NotFound();
			}

			this.groups.Delete(groupToDelete);

			return this.NoContent();
		}
	}
}
