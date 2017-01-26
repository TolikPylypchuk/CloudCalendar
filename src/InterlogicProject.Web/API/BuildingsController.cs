﻿using System.Collections.Generic;
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
	/// An API for buildings.
	/// </summary>
	[Route("api/[controller]")]
	public class BuildingsController : Controller
	{
		private IRepository<Building> buildings;

		/// <summary>
		/// Initializes a new instance of the BuildingsController class.
		/// </summary>
		/// <param name="repo">
		/// The repository that this instance will use.
		/// </param>
		public BuildingsController(IRepository<Building> repo)
		{
			this.buildings = repo;
		}

		/// <summary>
		/// Gets all buildings from the database.
		/// </summary>
		/// <returns>All buildings from the database.</returns>
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK,
			Type = typeof(IEnumerable<BuildingDto>))]
		public IEnumerable<BuildingDto> Get()
			=> this.buildings.GetAll()?.ProjectTo<BuildingDto>();

		/// <summary>
		/// Gets a buildings with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the building to get.</param>
		/// <returns>A building with the specified ID.</returns>
		[HttpGet("id/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(BuildingDto))]
		public BuildingDto Get(int id)
			=> Mapper.Map<BuildingDto>(this.buildings.GetById(id));
	}
}