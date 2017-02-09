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
	/// An API for groups.
	/// </summary>
	[Route("api/[controller]")]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> Get()
			=> this.groups.GetAll()?.ProjectTo<GroupDto>();

		/// <summary>
		/// Gets a group with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the group to get.</param>
		/// <returns>A group with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(GroupDto))]
		public GroupDto Get(int id)
			=> Mapper.Map<GroupDto>(this.groups.GetById(id));

		/// <summary>
		/// Gets all groups with the specified department.
		/// </summary>
		/// <param name="id">The ID of the department.</param>
		/// <returns>All groups with the specified department.</returns>
		[HttpGet("departmentId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
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
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupDto>))]
		public IEnumerable<GroupDto> GetForClass(int id)
			=> this.groups.GetAll()
						 ?.Include(g => g.Classes)
						  .Where(g => g.Classes.Any(c => c.ClassId == id))
						  .ProjectTo<GroupDto>();
	}
}
