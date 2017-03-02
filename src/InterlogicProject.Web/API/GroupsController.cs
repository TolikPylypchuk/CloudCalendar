using System.Collections.Generic;
using System.Linq;

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
	/// An API for groups.
	/// </summary>
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
		public IEnumerable<GroupDto> Get()
			=> this.groups.GetAll()?.ProjectTo<GroupDto>();

		/// <summary>
		/// Gets a group with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the group to get.</param>
		/// <returns>A group with the specified ID.</returns>
		[HttpGet("{id}", Name = "GetGroupById")]
		[SwaggerResponse(200, Type = typeof(GroupDto))]
		public GroupDto Get(int id)
			=> Mapper.Map<GroupDto>(this.groups.GetById(id));

		/// <summary>
		/// Gets all groups with the specified department.
		/// </summary>
		/// <param name="id">The ID of the department.</param>
		/// <returns>All groups with the specified department.</returns>
		[HttpGet("departmentId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForDepartment(int id)
			=> this.groups.GetAll()
						 ?.Where(g => g.Curator.DepartmentId == id)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified faculty.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <returns>All groups with the specified faculty.</returns>
		[HttpGet("facultyId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForFaculty(int id)
			=> this.groups.GetAll()
						 ?.Where(g => g.Curator.Department.FacultyId == id)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified enrollment year.
		/// </summary>
		/// <param name="year">The enrollment year.</param>
		/// <returns>All groups with the specified  enrollment year.</returns>
		[HttpGet("year/{year}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForYear(int year)
			=> this.groups.GetAll()
						 ?.Where(g => g.Year == year)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified faculty and enrollment year.
		/// </summary>
		/// <param name="id">The ID of the faculty.</param>
		/// <param name="year">The enrollment year.</param>
		/// <returns>
		/// All groups with the specified faculty and enrollment year.
		/// </returns>
		[HttpGet("facultyId/{id}/year/{year}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForFaculty(int id, int year)
			=> this.groups.GetAll()
						 ?.Where(g => g.Year == year &&
									  g.Curator.Department.FacultyId == id)
						  .ProjectTo<GroupDto>();

		/// <summary>
		/// Gets all groups with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All groups with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(200, Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForClass(int id)
			=> this.groups.GetAll()
						 ?.Include(g => g.Classes)
						  .Where(g => g.Classes.Any(c => c.ClassId == id))
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

			return this.CreatedAtRoute(
				"GetGroupById", new { id = groupDto.Id }, groupDto);
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
		public IActionResult Put(int id, [FromBody] GroupDto groupDto)
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
		public IActionResult Patch(int id, [FromBody] GroupDto groupDto)
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
		public IActionResult Delete(int id)
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
