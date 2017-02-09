using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Swashbuckle.SwaggerGen.Annotations;

using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Web.Models.Dto;

namespace InterlogicProject.Web.API
{
	/// <summary>
	/// An API for group-class relationships.
	/// </summary>
	[Route("api/[controller]")]
	public class GroupsClassesController : Controller
	{
		private IRepository<GroupClass> groupsClasses;

		/// <summary>
		/// Initializes a new instance of the GroupsClassesController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public GroupsClassesController(IRepository<GroupClass> repo)
		{
			this.groupsClasses = repo;
		}

		/// <summary>
		/// Gets all group-class relationships from the database.
		/// </summary>
		/// <returns>
		/// All group-class relationships from the database.
		/// </returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupClassDto>))]
		public IEnumerable<GroupClassDto> Get()
			=> this.groupsClasses.GetAll()?.ProjectTo<GroupClassDto>();

		/// <summary>
		/// Gets a group-class relationship with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the relationship to get.</param>
		/// <returns>A relationship with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(GroupClassDto))]
		public GroupClassDto Get(int id)
			=> Mapper.Map<GroupClassDto>(this.groupsClasses.GetById(id));

		/// <summary>
		/// Gets all group-class relationships with the specified class.
		/// </summary>
		/// <param name="id">The ID of the class.</param>
		/// <returns>All relationships with the specified class.</returns>
		[HttpGet("classId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupClassDto>))]
		public IEnumerable<GroupClassDto> GetForClass(int id)
			=> this.groupsClasses.GetAll()
								?.Where(gc => gc.ClassId == id)
								 .ProjectTo<GroupClassDto>();

		/// <summary>
		/// Gets all group-class relationships with the specified group.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <returns>All relationships with the specified group.</returns>
		[HttpGet("groupId/{id}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<GroupClassDto>))]
		public IEnumerable<GroupClassDto> GetForLecturer(int id)
			=> this.groupsClasses.GetAll()
								?.Where(lc => lc.GroupId == id)
								 .ProjectTo<GroupClassDto>();

		/// <summary>
		/// Gets all group-class relationships with the specified group
		/// between the specified dates.
		/// </summary>
		/// <param name="id">The ID of the group.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>
		/// All relationships with the specified group
		/// between the specified dates.
		/// </returns>
		[HttpGet("groupId/{id}/range/{start}/{end}")]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<LecturerClassDto>))]
		public IEnumerable<LecturerClassDto> GetForLecturerWithRange(
			int id,
			DateTime start,
			DateTime end)
			=> this.groupsClasses.GetAll()
								?.Where(gc => gc.GroupId == id &&
												 gc.Class.DateTime >= start &&
												 gc.Class.DateTime <= end)
								 .ProjectTo<LecturerClassDto>();
	}
}
